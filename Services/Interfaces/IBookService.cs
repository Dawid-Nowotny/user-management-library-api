using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IBookService
	{
		Task<IEnumerable<BookDto>> GetAllBooksAsync();
		Task<IEnumerable<BookDto>> GetBookInfoAsync(string identifier);
		Task<IEnumerable<BookDto>> GetFilteredAndSortedBooksAsync(FilterBooksDto filter);
		Task<PagedResult<BookDto>> GetPagedBooksAsync(PagedBooksDto pagedBooksDto);
		Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm);
	}
}
