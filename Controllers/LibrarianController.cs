using library_api.DTOs;
using library_api.Exceptions;
using library_api.Services;
using library_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
	[Authorize(Roles = "Admin,Librarian")]
	[ApiController]
	[Route("api/librarian")]
	public class LibrarianController : ControllerBase
	{
		private readonly ILibrarianService _librarianServices;
		private readonly IBookRentalService _bookRentalService;

		public LibrarianController(ILibrarianService librarianServices, IBookRentalService bookRentalService)
		{
			_librarianServices = librarianServices;
			_bookRentalService = bookRentalService;
		}

		[HttpGet("rentals/all")]
		public async Task<IActionResult> GetAllRentals()
		{
			var rentals = await _bookRentalService.GetAllRentalsAsync();
			return Ok(rentals);
		}

		[HttpGet("rentals/user/{identifier}")]
		public async Task<IActionResult> GetRentalsByUser(string identifier, [FromQuery] bool? isReturned = null)
		{
			try
			{
				var rentals = await _bookRentalService.GetRentalsByUserAsync(identifier, isReturned);
				return Ok(rentals);
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
		}

		[HttpPost("book")]
		public async Task<IActionResult> AddBook([FromBody] CreateBookDto createBookDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				await _librarianServices.AddAsync(createBookDto);
				return Ok("The book has been added.");
			}
			catch (DuplicateIsbnException e)
			{
				return Conflict(e.Message);
			}
		}

		[HttpPatch("book/copies")]
		public async Task<IActionResult> UpdateBookCopies([FromBody] UpdateBookCopiesDto updateBookCopiesDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				await _librarianServices.UpdateBookCopiesAsync(updateBookCopiesDto);
				return Ok("The number of copies has been updated.");
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (InvalidOperationException e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("book/{isbn}")]
		public async Task<IActionResult> DeleteUser(string ISBN)
		{
			try
			{
				await _librarianServices.DeleteBookAsync(ISBN);
				return NoContent();
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (BookDeletionException e)
			{
				return Conflict(e.Message);
			}
		}

		[HttpPut("book")]
		public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto updateBookDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				await _librarianServices.UpdateBookAsync(updateBookDto);
				return Ok("The book has been updated.");
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (InvalidOperationException e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPatch("rentals/return")]
		public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnRequest)
		{
			try
			{
				await _bookRentalService.ReturnBookAsync(returnRequest.Username, returnRequest.ISBN);
				return Ok("The book has been successfully returned.");
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
