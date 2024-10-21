using library_api.DTOs;
using library_api.Exceptions;
using library_api.Models;
using library_api.Services;
using library_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
	[ApiController]
	[Route("api/user")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPut("update")]
		[Authorize]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
		{
			var username = User.Identity.Name;

			if (string.IsNullOrEmpty(username))
			{
				return BadRequest("Username not found in token.");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				await _userService.UpdateUserAsync(username, updateUserDto);
				return NoContent();
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				return StatusCode(403, e.Message);
			}
			catch (InvalidOperationException e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("delete")]
		[Authorize]
		public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDto deleteUserDto)
		{
			var username = User.Identity.Name;

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (string.IsNullOrEmpty(username))
			{
				return BadRequest("Username not found in token.");
			}

			try
			{
				await _userService.DeleteUserAsync(username, deleteUserDto);
				return NoContent();
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				return StatusCode(403, e.Message);
			}
			catch (UserDeletionException e)
			{
				return Conflict(e.Message);
			}
		}
	}
}
