namespace library_api.DTOs
{
	public class BookRentalDto
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string BookTitle { get; set; }
		public string ISBN { get; set; }
		public string RentalDate { get; set; }
		public string ReturnDate { get; set; }
		public bool IsReturned { get; set; }
	}
}
