namespace library_api.Exceptions
{
	public class DuplicateIsbnException : Exception
	{
		public DuplicateIsbnException(string message) : base(message) { }
	}
}
