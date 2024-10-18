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
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime CreatedAt { get; set; }
		public UserRole Role { get; set; } = UserRole.User;
		public ICollection<BookRental> BookRental { get; set; }
	}
}