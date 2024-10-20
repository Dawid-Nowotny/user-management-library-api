using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IAdminService
	{
		Task<IEnumerable<UserDto>> GetAllUsersAsync();
		Task<UserDto> GetUserInfoAsync(string identifier);
		Task DeleteUserAsync(string identifier);
	}
}
