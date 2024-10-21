using library_api.DTOs;
using library_api.Exceptions;
using library_api.Models;
using library_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
	[Authorize(Roles = "Admin,Librarian")]
	[ApiController]
	[Route("api/admin")]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService _adminService;

		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}

		[HttpGet("all-users")]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			var users = await _adminService.GetAllUsersAsync();
			return Ok(users);
		}

		[HttpGet("user/{identifier}")]
		public async Task<IActionResult> GetUserInfo(string identifier)
		{
			try
			{
				var user = await _adminService.GetUserInfoAsync(identifier);
				return Ok(user);
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
		}

		[HttpDelete("user/{identifier}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteUser(string identifier)
		{
			try
			{
				await _adminService.DeleteUserAsync(identifier);
				return NoContent();
			}
			catch (KeyNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (UserDeletionException e)
			{
				return Conflict(e.Message);
			}
		}

		[HttpPut("user/{identifier}/role")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ChanChangeUserRole(string identifier, [FromBody] ChangeUserRoleDto newRole)
		{
			try
			{
				await _adminService.ChangeUserRoleAsync(identifier, newRole);
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
		}
	}
}
