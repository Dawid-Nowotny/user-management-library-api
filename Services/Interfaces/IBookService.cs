using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IBookService
	{
		Task<IEnumerable<BookDto>> GetBookInfoAsync(string identifier);
	}
}
