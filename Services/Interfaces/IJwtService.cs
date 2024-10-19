using library_api.Models;
using System.Security.Claims;

namespace library_api.Services.Interfaces
{
	public interface IJwtService
	{
		string GenerateToken(User user, TimeSpan expiration);
		ClaimsPrincipal ValidateToken(string token);
		ClaimsPrincipal? ValidateRefreshToken(string token);
	}
}
