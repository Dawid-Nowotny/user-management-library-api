using library_api.Models;

namespace library_api.Repositories.Interfaces
{
	public interface IBookRepository
	{
		Task AddAsync(Book book);
	}
}
