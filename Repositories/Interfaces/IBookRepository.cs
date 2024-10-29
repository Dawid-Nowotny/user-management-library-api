using library_api.DTOs;
using library_api.Models;

namespace library_api.Repositories.Interfaces
{
	public interface IBookRepository
	{
		Task AddAsync(Book book);
		Task<IEnumerable<Book>> GetAllBooksAsync();
		Task<IEnumerable<Book?>> GetBookInfoByISBNAsync(string ISBN);
		Task<Book?> GetBookByIsbnAsync(string ISBN);
		Task UpdateAsync(Book book);
		Task<bool> DeleteAsync(Book book);
		Task<IEnumerable<Book>> GetFilteredAndSortedBooksAsync(string title = null, string author = null, string isbn = null, string sortBy = null);
		Task<PagedResult<Book>> GetPagedBooksAsync(int pageNumber, int pageSize);
		Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
	}
}
