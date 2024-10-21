using System.ComponentModel.DataAnnotations;

namespace library_api.DTOs
{
	public class DeleteUserDto
	{
		[Required]
		public string CurrentPassword { get; set; }
	}
}
