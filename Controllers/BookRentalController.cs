using library_api.DTOs;
using library_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
	}
}
