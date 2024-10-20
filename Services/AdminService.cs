using AutoMapper;
using library_api.DTOs;
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

		public async Task<User> GetUserInfoAsync(string identifier)
		{
			var user = await _userRepository.GetUserInfoAsync(identifier);

			if (user == null)
			{
				throw new KeyNotFoundException("User not found with the provided identifier.");
			}

			return user;
		}
	}
}
