using library_api.Models;

namespace library_api.Repositories.Interfaces
{
	public interface IBookRentalRepository
	{
		Task<IEnumerable<BookRental>> GetAllRentalsAsync();
		Task<IEnumerable<BookRental>> GetRentalsByUserAsync(int userId, bool? isReturned = null);
		Task<BookRental?> GetActiveRentalByUserAndIsbnAsync(int userId, string isbn);
		Task AddAsync(BookRental rental);
		Task<bool> BookAlreadyRentedByUserAsync(int userId, int bookId);
		Task UpdateAsync(BookRental bookRental);
	}
}
