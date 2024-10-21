using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IAuthService
	{
		Task RegisterAsync(RegisterUserDto registerUserDto);
		Task<(string accessToken, string refreshToken)> LoginAsync(LoginUserDto loginUserDto);
		Task<string> RefreshTokenAsync(string refreshToken);
		string HashPassword(string password);
		bool VerifyPassword(string inputPassword, string storedPasswordHash);
	}
}
