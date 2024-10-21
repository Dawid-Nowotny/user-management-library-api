using AutoMapper;
using library_api.DTOs;
using library_api.Exceptions;
using library_api.Models;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;

namespace library_api.Services
{
	public class AdminService : IAdminService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public AdminService(IUserRepository userRepository, IMapper mapper) 
		{ 
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
		{
			var users = await _userRepository.GetAllUsersAsync();

			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task<UserDto> GetUserInfoAsync(string identifier)
		{
			var user = await _userRepository.GetUserInfoAsync(identifier);

			if (user == null)
			{
				throw new KeyNotFoundException("User not found with the provided identifier.");
			}

			return _mapper.Map<UserDto>(user);
		}

		public async Task ChangeUserRoleAsync(string identifier, ChangeUserRoleDto changeUserRoleDto)
		{
			var user = await _userRepository.GetUserInfoAsync(identifier);

			if (user == null)
			{
				throw new KeyNotFoundException("User not found with the provided identifier.");
			}

			if (user.Role == UserRole.Admin && changeUserRoleDto.NewRole != UserRole.Admin)
			{
				throw new UnauthorizedAccessException("Cannot downgrade an Admin to another role.");
			}

			if (changeUserRoleDto.NewRole == UserRole.Admin)
			{
				throw new UnauthorizedAccessException("Cannot assign Admin role to another user.");
			}

			user.Role = changeUserRoleDto.NewRole;
			await _userRepository.UpdateAsync(user);
		}

		public async Task DeleteUserAsync(string identifier)
		{
			var user = await _userRepository.GetUserInfoAsync(identifier);

			if (user == null)
			{
				throw new KeyNotFoundException("User not found.");
			}

			if (user.Role == UserRole.Admin)
			{
				throw new UserDeletionException("Cannot delete an administrator.");
			}

			bool deleted = await _userRepository.DeleteAsync(user);

			if (!deleted)
			{
				throw new UserDeletionException("Failed to delete the user");
			}
		}
	}
}
