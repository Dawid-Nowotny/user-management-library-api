using library_api.DTOs;
using library_api.Models;

namespace library_api.Services.Interfaces
{
	public interface IAdminService
	{
		Task<IEnumerable<UserDto>> GetAllUsersAsync();
		Task<UserDto> GetUserInfoAsync(string identifier);
		Task ChangeUserRoleAsync(string identifier, ChangeUserRoleDto newRole);
		Task DeleteUserAsync(string identifier);
	}
}
