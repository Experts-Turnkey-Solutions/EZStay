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
            CreateMap<User, UserDto>();

            // Property <-> PropertyDto
            CreateMap<Property, PropertyDto>().ReverseMap();

            // Booking <-> BookingDto
            CreateMap<Booking, BookingDto>().ReverseMap();

            // Review <-> ReviewDto
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
