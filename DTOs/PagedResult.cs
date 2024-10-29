namespace library_api.DTOs
{
	public class PagedResult<T>
	{
		public IEnumerable<T> Items { get; set; }
		public int TotalCount { get; set; }

		public PagedResult()
		{
			Items = new List<T>();
		}
	}
}
