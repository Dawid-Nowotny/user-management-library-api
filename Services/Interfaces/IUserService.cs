using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IUserService
	{
		Task UpdateUserAsync(string username, UpdateUserDto updateUserDto);
		Task DeleteUserAsync(string username, DeleteUserDto deleteUserDto);
	}
}
