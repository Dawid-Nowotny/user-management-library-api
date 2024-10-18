using library_api.DTOs;

namespace library_api.Services.Interfaces
{
	public interface IAuthService
	{
		Task RegisterAsync(RegisterUserDto registerUserDto);
	}
}
