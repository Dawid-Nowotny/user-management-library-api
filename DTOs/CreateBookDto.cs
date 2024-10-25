using System.ComponentModel.DataAnnotations;

namespace library_api.DTOs
{
	public class CreateBookDto
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		[DataType(DataType.Date)]
		public DateTime PublishedDate { get; set; }
		public int CopiesAvailable { get; set; }
	}
}
