using library_api.DTOs;
using library_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace library_api.Controllers
{
	[ApiController]
	[Route("api/rentals")]
	public class BookRentalController : ControllerBase
	{
		private readonly IBookRentalService _bookRentalService;

		public BookRentalController(IBookRentalService bookRentalService)
		{
			_bookRentalService = bookRentalService;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetMyRentals([FromQuery] bool? isReturned = null)
		{
			try
			{
				var username = User.Identity.Name;
				var rentals = await _bookRentalService.GetUserRentalsAsync(username, isReturned);
				return Ok(rentals);
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				return StatusCode(403, e.Message);

			}
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> RentBook([FromBody] RentBookDto request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var username = User.Identity.Name;
				await _bookRentalService.RentBookAsync(request, username);
				return Ok("The book has been successfully rented.");
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (InvalidOperationException e)
			{
				return Conflict(e.Message);
			}
		}

		[HttpPatch("extend")]
		[Authorize]
		public async Task<IActionResult> ExtendRental([FromBody] ExtendRentalDto extendRequest)
		{
			try
			{
				var username = User.Identity.Name;
				await _bookRentalService.ExtendRentalByIsbnAsync(username, extendRequest.ISBN);
				return Ok("The rental has been successfully extended by 7 days.");
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (InvalidOperationException e)
			{
				return Conflict(e.Message);
			}
		}
	}
}
