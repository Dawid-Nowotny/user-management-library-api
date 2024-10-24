using AutoMapper;
using library_api.DTOs;
using library_api.Exceptions;
using library_api.Models;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.SqlClient;

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
			var book = _mapper.Map<Book>(createBookDto);

			try
			{
				await _bookRepository.AddAsync(book);
			}
			catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx &&
												pgEx.SqlState == "23505")
			{
				throw new DuplicateIsbnException($"A book with ISBN {createBookDto.ISBN} already exists.");
			}
		}
	}
}
