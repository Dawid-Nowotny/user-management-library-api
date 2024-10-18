using library_api.DTOs;
using library_api.Exceptions;
using library_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
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
	}
}
