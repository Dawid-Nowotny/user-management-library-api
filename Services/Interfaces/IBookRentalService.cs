using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IBookRentalService
	{
		Task<IEnumerable<BookRentalDto>> GetAllRentalsAsync();
		Task<IEnumerable<BookRentalDto>> GetRentalsByUserAsync(string identifier, bool? isReturned = null);
		Task<IEnumerable<BookRentalDto>> GetUserRentalsAsync(string username, bool? isReturned = null);
		Task RentBookAsync(RentBookDto rentBookDto, string username);
		Task ExtendRentalByIsbnAsync(string username, string ISBN);
		Task ReturnBookAsync(string username, string ISBN);
	}
}
