﻿using library_api.DTOs;
using library_api.Models;
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

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<Book>>> GetUsers()
		{
			var books = await _bookService.GetAllBooksAsync();
			return Ok(books);
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

		[HttpGet("filter")]
		public async Task<IActionResult> FilterAndSortBooks([FromQuery] FilterBooksDto filter)
		{
			var books = await _bookService.GetFilteredAndSortedBooksAsync(filter);
			return Ok(books);
		}

		[HttpGet("paged")]
		public async Task<IActionResult> GetPagedBooks([FromQuery] PagedBooksDto pagedBooksDto)
		{
			var pagedResult = await _bookService.GetPagedBooksAsync(pagedBooksDto);
			return Ok(pagedResult);
		}

		[HttpGet("search")]
		public async Task<IActionResult> SearchBooks([FromQuery] string searchTerm)
		{
			var books = await _bookService.SearchBooksAsync(searchTerm);
			return Ok(books);
		}
	}
}
