using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IBookService
	{
		Task<IEnumerable<BookDto>> GetAllBooksAsync();
		Task<IEnumerable<BookDto>> GetBookInfoAsync(string identifier);
		Task<IEnumerable<BookDto>> GetFilteredAndSortedBooksAsync(string title, string author, string isbn, string sortBy);
		Task<PagedResult<BookDto>> GetPagedBooksAsync(int pageNumber, int pageSize);
		Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm);
	}
}
