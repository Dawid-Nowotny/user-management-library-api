using AutoMapper;
using library_api.DTOs;
using library_api.Models;

namespace library_api.Profiles
{
	public class BookRentalMappingProfile : Profile
	{
		public BookRentalMappingProfile()
		{
            CreateMap<RentBookDto, BookRental>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.BookId, opt => opt.Ignore())
                .ForMember(dest => dest.RentalDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddDays(30)));

			CreateMap<BookRental, BookRentalDto>()
				.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
				.ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
				.ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.Book.ISBN))
				.ForMember(dest => dest.RentalDate, opt =>
					opt.MapFrom(src => src.RentalDate.ToString("yyyy-MM-dd")))
				.ForMember(dest => dest.ReturnDate, opt =>
					opt.MapFrom(src => src.ReturnDate.ToString("yyyy-MM-dd")));
		}
	}
}
