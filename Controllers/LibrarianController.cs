using library_api.DTOs;
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
		public LibrarianController(ILibrarianService librarianServices) 
		{ 
			_librarianServices = librarianServices;
		}

		[HttpPost("book")]
		public async Task<IActionResult> AddBook([FromBody] CreateBookDto createBookDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _librarianServices.AddAsync(createBookDto);
			return Ok("The book has been added.");
		}
	}
}
