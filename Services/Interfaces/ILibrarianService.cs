using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface ILibrarianService
	{
		Task AddAsync(CreateBookDto createBookDto);
		Task UpdateBookCopiesAsync(UpdateBookCopiesDto updateBookCopiesDto);
		Task DeleteBookAsync(string ISBN);
		Task UpdateBookAsync(UpdateBookDto updateBookDto);
	}
}
