using library_api.Data;
using library_api.DTOs;
using library_api.Models;
using library_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace library_api.Repositories
{
	public class BookRepository : IBookRepository
	{
		public readonly ApplicationDbContext _context;

		public BookRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Book book)
		{
			_context.Books.Add(book);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Book>> GetAllBooksAsync()
		{
			return await _context.Books.ToListAsync();
		}

		public async Task<IEnumerable<Book?>> GetBookInfoByISBNAsync(string ISBN)
		{
			var bookByIsbn = await _context.Books.FirstOrDefaultAsync(b => b.ISBN.ToLower() == ISBN.ToLower());
			if (bookByIsbn != null)
			{
				return new List<Book> { bookByIsbn };
			}

			return await _context.Books.Where(b => b.Title.ToLower() == ISBN.ToLower()).ToListAsync();
		}

		public async Task<Book?> GetBookByIsbnAsync(string ISBN)
		{
			return await _context.Books.FirstOrDefaultAsync(b => b.ISBN.ToLower() == ISBN.ToLower());
		}

		public async Task UpdateAsync(Book book)
		{
			_context.Books.Update(book);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteAsync(Book book)
		{
			if (book != null)
			{
				_context.Books.Remove(book);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<IEnumerable<Book>> GetFilteredAndSortedBooksAsync(string title = null, string author = null, string isbn = null, string sortBy = null)
		{
			var query = _context.Books.AsQueryable();

			if (!string.IsNullOrEmpty(title))
			{
				query = query.Where(b => b.Title.Contains(title));
			}

			if (!string.IsNullOrEmpty(author))
			{
				query = query.Where(b => b.Author.Contains(author));
			}

			if (!string.IsNullOrEmpty(isbn))
			{
				query = query.Where(b => b.ISBN == isbn);
			}

			switch (sortBy?.ToLower())
			{
				case "title":
					query = query.OrderBy(b => b.Title);
					break;
				case "author":
					query = query.OrderBy(b => b.Author);
					break;
				case "isbn":
					query = query.OrderBy(b => b.ISBN);
					break;
				default:
					break;
			}

			return await query.ToListAsync();
		}

		public async Task<PagedResult<Book>> GetPagedBooksAsync(int pageNumber, int pageSize)
		{
			var totalCount = await _context.Books.CountAsync();
			var items = await _context.Books
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new PagedResult<Book>
			{
				Items = items,
				TotalCount = totalCount
			};
		}

		public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
		{
			return await _context.Books
				.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm) || b.ISBN.Contains(searchTerm))
				.ToListAsync();
		}
	}
}
