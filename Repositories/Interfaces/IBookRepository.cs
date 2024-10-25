using library_api.Models;

namespace library_api.Repositories.Interfaces
{
	public interface IBookRepository
	{
		Task AddAsync(Book book);
		Task<IEnumerable<Book>> GetAllBooksAsync();
		Task<IEnumerable<Book?>> GetBookInfoAsync(string identifier);
		Task<Book?> GetBookByIsbnAsync(string ISBN);
		Task UpdateAsync(Book book);
		Task<bool> DeleteAsync(Book book);
	}
}
