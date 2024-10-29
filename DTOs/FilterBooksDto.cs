namespace library_api.DTOs
{
	public class FilterBooksDto
	{
		public string? Title { get; set; } = null;
		public string? Author { get; set; } = null;
		public string? ISBN { get; set; } = null;
		public string? SortBy { get; set; } = null;
	}
}
