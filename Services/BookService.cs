﻿using AutoMapper;
using library_api.DTOs;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;

namespace library_api.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _bookRepository;
		private readonly IMapper _mapper;

		public BookService(IBookRepository bookRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
		{
			var users = await _bookRepository.GetAllBooksAsync();

			return _mapper.Map<IEnumerable<BookDto>>(users);
		}

		public async Task<IEnumerable<BookDto>> GetBookInfoAsync(string identifier)
		{
			var books = await _bookRepository.GetBookInfoByISBNAsync(identifier);

			if (books == null || !books.Any())
			{
				throw new KeyNotFoundException("Book not found with the provided identifier.");
			}

			return books.Select(book => _mapper.Map<BookDto>(book)).ToList();
		}

		public async Task<IEnumerable<BookDto>> GetFilteredAndSortedBooksAsync(string title, string author, string isbn, string sortBy)
		{
			var books = await _bookRepository.GetFilteredAndSortedBooksAsync(title, author, isbn, sortBy);
			return _mapper.Map<IEnumerable<BookDto>>(books);
		}

		public Task<PagedResult<BookDto>> GetPagedBooksAsync(int pageNumber, int pageSize)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm)
		{
			throw new NotImplementedException();
		}
	}
}
