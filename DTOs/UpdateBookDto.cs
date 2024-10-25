namespace library_api.DTOs
{
	public class UpdateBookDto
	{
		public string ISBN { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public DateTime PublishedDate { get; set; }
	}
}
