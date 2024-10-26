using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IBookRentalService
	{
		Task<IEnumerable<BookRentalDto>> GetAllRentalsAsync();
		Task RentBookAsync(RentBookDto rentBookDto, string username);
	}
}
