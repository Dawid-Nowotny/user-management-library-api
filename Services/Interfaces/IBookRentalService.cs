using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IBookRentalService
	{
		Task RentBookAsync(RentBookDto rentBookDto);
	}
}
