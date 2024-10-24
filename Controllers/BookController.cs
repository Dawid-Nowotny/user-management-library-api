using library_api.Services;
using library_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
	[ApiController]
	[Route("api/book")]
	public class BookController : ControllerBase
	{
		private readonly IBookService _bookService;

		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}

		[HttpGet("{identifier}")]
		public async Task<IActionResult> GetBookInfo(string identifier)
		{
			try
			{
				var books = await _bookService.GetBookInfoAsync(identifier);

				if (books.Count() > 1)
				{
					return Ok(new { Message = "Multiple books found", Books = books });
				}

				return Ok(books.First());
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
		}
	}
}
