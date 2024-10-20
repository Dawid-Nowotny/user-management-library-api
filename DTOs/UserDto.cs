using library_api.Converters;
using System.Text.Json.Serialization;

namespace library_api.DTOs
{
	public class UserDto
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }

		[JsonConverter(typeof(DateTimeJsonConverter))]
		public DateTime CreatedAt { get; set; }
	}
}
