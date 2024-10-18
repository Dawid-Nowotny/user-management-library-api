using library_api.DTOs;
using library_api.Exceptions;
using library_api.Models;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;

namespace library_api.Services
{
	public class AuthService : IAuthService
	{
		private IUserRepository _userRepository;

		public AuthService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task RegisterAsync(RegisterUserDto registerUserDto)
		{
			var existingUser = await _userRepository.GetByEmailAsync(registerUserDto.Email);
			if (existingUser != null)
			{
				throw new UserAlreadyExistsException("A user with the given email already exists.");
			}

			var existingUserByUsername = await _userRepository.GetByUsernameAsync(registerUserDto.Username);
			if (existingUserByUsername != null)
			{
				throw new UserAlreadyExistsException("A user with the given username already exists.");
			}

			var hashedPassword = HashPassword(registerUserDto.Password);

			var user = new User
			{
				Username = registerUserDto.Username,
				Email = registerUserDto.Email,
				Password = hashedPassword,
				CreatedAt = DateTime.UtcNow,
				Role = UserRole.User
			};

			await _userRepository.AddAsync(user);
		}

		string HashPassword(string password)
		{
			using (var sha256 = System.Security.Cryptography.SHA256.Create())
			{
				var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return Convert.ToBase64String(bytes);
			}
		}
	}
}
