using library_api.Data;
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

		public async Task<IEnumerable<Book?>> GetBookInfoAsync(string identifier)
		{
			var bookByIsbn = await _context.Books.FirstOrDefaultAsync(b => b.ISBN.ToLower() == identifier.ToLower());
			if (bookByIsbn != null)
			{
				return new List<Book> { bookByIsbn };
			}

			return await _context.Books.Where(b => b.Title.ToLower() == identifier.ToLower()).ToListAsync();
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
	}
}
