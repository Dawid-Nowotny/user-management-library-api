using library_api.Models;
using System.ComponentModel.DataAnnotations;

namespace library_api.DTOs
{
	public class ChangeUserRoleDto
	{
		[Required]
		public UserRole NewRole { get; set; }
	}
}
