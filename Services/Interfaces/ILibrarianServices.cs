using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface ILibrarianServices
	{
		Task AddAsync(CreateBookDto createBookDto);
	}
}
