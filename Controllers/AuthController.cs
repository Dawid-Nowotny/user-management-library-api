using library_api.DTOs;
using library_api.Exceptions;
using library_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace library_api.Controllers
{
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				await _authService.RegisterAsync(registerUserDto);
				return Ok("The user has been registered.");
			}
			catch (UserAlreadyExistsException e)
			{
				return Conflict(e.Message);
			}
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var tokens = await _authService.LoginAsync(loginUserDto);
				
				Response.Cookies.Append("refreshToken", tokens.refreshToken, new CookieOptions
				{
					HttpOnly = true,
					Secure = true,
					SameSite = SameSiteMode.Strict,
					Expires = DateTime.UtcNow.AddDays(7)
				});

				return Ok(new
				{
					tokens.accessToken
				});
			}
			catch (UnauthorizedAccessException e)
			{
				return Conflict(e.Message);
			}
		}

		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken()
		{
			try
			{
				var refreshToken = Request.Cookies["refreshToken"];

				if (refreshToken == null)
				{
					return Unauthorized("Refresh token missing");
				}

				var newAccessToken = await _authService.RefreshTokenAsync(refreshToken);

				return Ok(new
				{
					newAccessToken
				});
			}
			catch (SecurityTokenException e)
			{
				return Conflict(e.Message);
			}
		}

		[HttpPost("logout")]
		public IActionResult Logout()
		{
			Response.Cookies.Append("refreshToken", "", new CookieOptions
			{
				Expires = DateTime.UtcNow.AddDays(-1),
			});

			return NoContent();
		}
	}
}
