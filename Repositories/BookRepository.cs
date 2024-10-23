using library_api.Data;
using library_api.Models;
using library_api.Repositories.Interfaces;

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
	}
}
