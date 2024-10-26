using library_api.Models;

namespace library_api.Repositories.Interfaces
{
	public interface IBookRentalRepository
	{
		Task<IEnumerable<BookRental>> GetAllRentalsAsync();
		Task AddAsync(BookRental rental);
		Task<bool> BookAlreadyRentedByUserAsync(int userId, int bookId);
	}
}
