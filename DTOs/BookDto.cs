using library_api.Converters;
using System.Text.Json.Serialization;

namespace library_api.DTOs
{
	public class BookDto
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		[JsonConverter(typeof(DateOnlyConverter))]
		public DateTime PublishedDate { get; set; }
		public int CopiesAvailable { get; set; }
	}
}
