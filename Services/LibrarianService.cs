using AutoMapper;
using library_api.DTOs;
using library_api.Exceptions;
using library_api.Models;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;

namespace library_api.Services
{
	public class LibrarianService : ILibrarianService
	{
		private readonly IBookRepository _bookRepository;
		private readonly IMapper _mapper;

		public LibrarianService(IBookRepository bookRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_mapper = mapper;
		}

		public async Task AddAsync(CreateBookDto createBookDto)
		{
			var existingBook = await _bookRepository.GetBookInfoAsync(createBookDto.ISBN);
			if (existingBook.Any())
			{
				throw new DuplicateIsbnException($"A book with ISBN {createBookDto.ISBN} already exists.");
			}

			var book = _mapper.Map<Book>(createBookDto);
			await _bookRepository.AddAsync(book);
		}
	}
}
