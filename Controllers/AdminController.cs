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

		[HttpGet("user")]
		public async Task<IActionResult> GetUserInfo(string identifier)
		{
			try
			{
				var user = await _adminService.GetUserInfoAsync(identifier);
				return Ok(user);
			}
			catch (KeyNotFoundException e)
			{
				return Conflict(e.Message);
			}
		}
	}
}
