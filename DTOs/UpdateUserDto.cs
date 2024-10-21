using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace library_api.DTOs
{
	public class UpdateUserDto
	{
		public string Username { get; set; }

		public string Email { get; set; }

		public string CurrentPassword { get; set; }

		public string NewPassword { get; set; }
	}
}
