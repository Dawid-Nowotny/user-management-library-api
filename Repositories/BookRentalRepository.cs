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
