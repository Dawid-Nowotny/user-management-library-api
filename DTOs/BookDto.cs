namespace library_api.DTOs
{
	public class BookDto
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		public DateTime PublishedDate { get; set; }
		public int CopiesAvailable { get; set; }
	}
}
