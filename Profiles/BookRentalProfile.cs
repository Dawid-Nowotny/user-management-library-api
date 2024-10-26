using AutoMapper;
using library_api.DTOs;
using library_api.Models;

namespace library_api.Profiles
{
	public class BookRentalProfile : Profile
	{
		public BookRentalProfile()
		{
			CreateMap<RentBookDto, BookRental>()
				.ForMember(dest => dest.UserId, opt => opt.Ignore())
				.ForMember(dest => dest.BookId, opt => opt.Ignore())
				.ForMember(dest => dest.RentalDate, opt => opt.MapFrom(src => DateTime.UtcNow))
				.ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => false));
		}
	}
}
