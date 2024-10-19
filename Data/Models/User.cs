using System.ComponentModel.DataAnnotations;

namespace library_api.Models
{
	public enum UserRole
	{
		Admin,
		Librarian,
		User,
	}

	public class User
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Username is required.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
		public string Password { get; set; }
		public DateTime CreatedAt { get; set; }
		public UserRole Role { get; set; } = UserRole.User;
		public ICollection<BookRental> BookRental { get; set; }
	}
}