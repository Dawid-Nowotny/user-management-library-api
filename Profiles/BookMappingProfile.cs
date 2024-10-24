using AutoMapper;
using library_api.DTOs;
using library_api.Models;

namespace library_api.Profiles
{
	public class BookMappingProfile : Profile
	{
		public BookMappingProfile()
		{
			CreateMap<CreateBookDto, Book>()
				.ForMember(dest => dest.BookRental, opt => opt.MapFrom(src => new List<BookRental>()))
				.ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

			CreateMap<Book, BookDto>();
		}
	}
}
