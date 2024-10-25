using library_api.DTOs;
using library_api.Exceptions;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;

namespace library_api.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IAuthService _authService;

		public UserService(IUserRepository userRepository, IAuthService authService)
		{
			_userRepository = userRepository;
			_authService = authService;
		}

		public async Task DeleteUserAsync(string username, DeleteUserDto deleteUserDto)
		{
			var user = await _userRepository.GetByUsernameAsync(username);
			if (user == null)
			{
				throw new KeyNotFoundException("User not found.");
			}

			if (!_authService.VerifyPassword(deleteUserDto.CurrentPassword, user.Password))
			{
				throw new UnauthorizedAccessException("Invalid current password.");
			}

			bool deleted = await _userRepository.DeleteAsync(user);

			if (!deleted)
			{
				throw new UserDeletionException("Failed to delete the user");
			}
		}

		public async Task UpdateUserAsync(string username, UpdateUserDto updateUserDto)
		{
			var user = await _userRepository.GetByUsernameAsync(username);
			if (user == null)
			{
				throw new KeyNotFoundException("User not found.");
			}

			if (!_authService.VerifyPassword(updateUserDto.CurrentPassword, user.Password))
			{
				throw new UnauthorizedAccessException("Invalid current password.");
			}

			bool isUpdated = false;

			if (!string.IsNullOrEmpty(updateUserDto.Username) && updateUserDto.Username != user.Username)
			{
				user.Username = updateUserDto.Username;
				isUpdated = true;
			}

			if (!string.IsNullOrEmpty(updateUserDto.Email) && updateUserDto.Email != user.Email)
			{
				user.Email = updateUserDto.Email;
				isUpdated = true;
			}

			if (!string.IsNullOrEmpty(updateUserDto.NewPassword))
			{
				var hashedNewPassword = _authService.HashPassword(updateUserDto.NewPassword);
				user.Password = hashedNewPassword;
				isUpdated = true;
			}

			if (!isUpdated)
			{
				throw new InvalidOperationException("No updates were made to the user.");
			}

			await _userRepository.UpdateAsync(user);
		}
	}
}
