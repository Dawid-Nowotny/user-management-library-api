using library_api.DTOs;
using library_api.Models;

namespace library_api.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<User?> GetByEmailAsync(string email);
		Task<User?> GetByUsernameAsync(string username);
		Task AddAsync(User user);
		Task<IEnumerable<User>> GetAllUsersAsync();
		Task<User?> GetUserInfoAsync(string identifier);
		Task<bool> DeleteAsync(User user);
		Task UpdateAsync(User user, UserRole newRole);
	}
}
