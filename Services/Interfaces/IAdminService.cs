using library_api.DTOs;
using library_api.Models;

namespace library_api.Services.Interfaces
{
	public interface IAdminService
	{
		Task<IEnumerable<UserDto>> GetAllUsersAsync();
		Task<User> GetUserInfoAsync(string identifier);

	}
}
