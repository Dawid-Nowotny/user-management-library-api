using AutoMapper;
using library_api.DTOs;
using library_api.Exceptions;
using library_api.Models;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace library_api.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IJwtService _jwtService;
		private readonly IMapper _mapper;


		public AuthService(IUserRepository userRepository, IJwtService jwtService, IMapper mapper)
		{
			_userRepository = userRepository;
			_jwtService = jwtService;
			_mapper = mapper;
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

			var user = _mapper.Map<User>(registerUserDto);
			user.Password = hashedPassword;

			await _userRepository.AddAsync(user);
		}

		public async Task<(string accessToken, string refreshToken)> LoginAsync(LoginUserDto loginUserDto)
		{
			var user = await _userRepository.GetByEmailAsync(loginUserDto.Email);
			if (user == null || !VerifyPassword(loginUserDto.Password, user.Password))
			{
				throw new UnauthorizedAccessException("Invalid credentials");
			}

			var accessToken = _jwtService.GenerateToken(user, TimeSpan.FromMinutes(15));
						var refreshToken = _jwtService.GenerateToken(user, TimeSpan.FromDays(7));


			return (accessToken, refreshToken);
		}

		public async Task<string> RefreshTokenAsync(string refreshToken)
		{
			var principal = _jwtService.ValidateRefreshToken(refreshToken);
			if (principal == null)
			{
				throw new SecurityTokenException("Invalid or expired refresh token");
			}

			var usernameClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
			if (usernameClaim == null)
			{
				throw new SecurityTokenException("Invalid token - no username found");
			}

			var user = await _userRepository.GetByUsernameAsync(usernameClaim);
			if (user == null)
			{
				throw new SecurityTokenException("User not found");
			}

			var newAccessToken = _jwtService.GenerateToken(user, TimeSpan.FromMinutes(15));

			return newAccessToken;
		}

		public string HashPassword(string password)
		{
			using (var sha256 = System.Security.Cryptography.SHA256.Create())
			{
				var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return Convert.ToBase64String(bytes);
			}
		}

		public bool VerifyPassword(string inputPassword, string storedPasswordHash)
		{
			using (var sha256 = System.Security.Cryptography.SHA256.Create())
			{
				var inputHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword)));
				return inputHash == storedPasswordHash;
			}
		}
	}
}
