using AutoMapper;
using library_api.DTOs;
using library_api.Exceptions;
using library_api.Models;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace library_api.Services
{
	public class LibrarianService : ILibrarianService
	{
		private readonly IBookRepository _bookRepository;
		private readonly IBookRentalRepository _rentalRepository;
		private readonly IMapper _mapper;

		public LibrarianService(IBookRepository bookRepository, IBookRentalRepository rentalRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_rentalRepository = rentalRepository;
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

		public async Task DeleteBookAsync(string ISBN)
		{
			var book = await _bookRepository.GetBookByIsbnAsync(ISBN);

			if (book == null)
			{
				throw new KeyNotFoundException("Book not found.");
			}

			var activeRentals = await _rentalRepository.GetRentalsByBookAsync(book.Id);
			if (activeRentals.Any(r => !r.IsReturned))
			{
				throw new InvalidOperationException("Cannot delete a book that is currently borrowed.");
			}

			bool deleted = await _bookRepository.DeleteAsync(book);

			if (!deleted)
			{
				throw new BookDeletionException("Failed to delete the book");
			}
		}

		public async Task UpdateBookAsync(UpdateBookDto updateBookDto)
		{
			var book = await _bookRepository.GetBookByIsbnAsync(updateBookDto.ISBN);

			if (book == null)
			{
				throw new KeyNotFoundException("Book not found.");
			}

			bool isUpdated = false;

			if (!string.IsNullOrEmpty(updateBookDto.Title) && updateBookDto.Title != book.Title)
			{
				book.Title = updateBookDto.Title;
				isUpdated = true;
			}

			if (!string.IsNullOrEmpty(updateBookDto.Author) && updateBookDto.Author != book.Author)
			{
				book.Author = updateBookDto.Author;
				isUpdated = true;
			}

			var updatePublishedDateUtc = DateTime.SpecifyKind(updateBookDto.PublishedDate.ToUniversalTime(), DateTimeKind.Utc);
			var bookPublishedDateUtc = DateTime.SpecifyKind(book.PublishedDate.ToUniversalTime(), DateTimeKind.Utc);

			if (updatePublishedDateUtc.Date != bookPublishedDateUtc.Date)
			{
				book.PublishedDate = updatePublishedDateUtc;
				isUpdated = true;
			}

			if (!isUpdated)
			{
				throw new InvalidOperationException("No updates were made to the book.");
			}

			await _bookRepository.UpdateAsync(book);
		}

		public async Task UpdateBookCopiesAsync(UpdateBookCopiesDto updateBookCopiesDto)
		{
			var book = await _bookRepository.GetBookByIsbnAsync(updateBookCopiesDto.ISBN);

			if (book == null)
			{
				throw new KeyNotFoundException("Book not found.");
			}

			int newCopiesAvailable = book.CopiesAvailable + updateBookCopiesDto.ChangeAmount;

			if (newCopiesAvailable < 0)
			{
				throw new InvalidOperationException("The number of copies cannot be negative.");
			}

			book.CopiesAvailable = newCopiesAvailable;

			await _bookRepository.UpdateAsync(book);
		}
	}
}
