using library_api.Data;
using library_api.Models;
using library_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace library_api.Repositories
{
	public class BookRentalRepository : IBookRentalRepository
	{
		private readonly ApplicationDbContext _context;

		public BookRentalRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<BookRental>> GetAllRentalsAsync()
		{
			return await _context.BookRentals
				.Include(r => r.User)
				.Include(r => r.Book)
				.ToListAsync();
		}

		public async Task<IEnumerable<BookRental>> GetRentalsByUserAsync(int userId, bool? isReturned = null)
		{
			var query = _context.BookRentals.AsQueryable();

			if (isReturned.HasValue)
			{
				query = query.Where(r => r.UserId == userId && r.IsReturned == isReturned.Value);
			}
			else
			{
				query = query.Where(r => r.UserId == userId);
			}

			return await query.Include(r => r.User).Include(r => r.Book).ToListAsync();
		}

		public async Task AddAsync(BookRental rental)
		{
			_context.BookRentals.Add(rental);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> BookAlreadyRentedByUserAsync(int userId, int bookId)
		{
			return await _context.BookRentals
				.AnyAsync(r => r.UserId == userId && r.BookId == bookId && !r.IsReturned);
		}
	}
}
