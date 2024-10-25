using AutoMapper;
using library_api.DTOs;
using library_api.Models;

namespace library_api.Profiles
{
	public class UserMappingProfile : Profile
	{
		public UserMappingProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

			CreateMap<RegisterUserDto, User>()
				.ForMember(dest => dest.Password, opt => opt.Ignore())
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.User));
		}
	}
}
