using AutoMapper;
using EZStay.Api.Models.Domain;
using EZStay.Api.Models.DTOs;

namespace EZStay.Api.Mappings
{
    public class AMProfiles : Profile
    {
        public AMProfiles()
        {
            // RegisterDto -> User
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // User -> UserDto
            CreateMap<User, UserDto>()
                // Roles is already List<string>, map directly without Split
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src => src.Roles));

            // UserDto -> User
            CreateMap<UserDto, User>()
                // Roles is List<string>, assign directly without Join
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src => src.Roles));

            // Property <-> PropertyDto
            CreateMap<Property, PropertyDto>().ReverseMap();

            // Booking <-> BookingDto
            CreateMap<Booking, BookingDto>().ReverseMap();

            // Review <-> ReviewDto
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }

}

