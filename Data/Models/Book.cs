namespace library_api.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		public DateTime PublishedDate { get; set; }
		public int CopiesAvailable { get; set; }
		public ICollection<BookRental> BookRental { get; set; }
	}
}