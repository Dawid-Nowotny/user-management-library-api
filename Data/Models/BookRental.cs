namespace library_api.Models
{
	public class BookRental
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public int UserId { get; set; }
		public DateTime RentalDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public bool IsReturned { get; set; }
		public bool IsExtended { get; set; }
		public User User { get; set; }
		public Book Book { get; set; }
	}
}