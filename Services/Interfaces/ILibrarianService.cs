using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface ILibrarianService
	{
		Task AddAsync(CreateBookDto createBookDto);
		Task DeleteBookAsync(string isbn);
	}
}
