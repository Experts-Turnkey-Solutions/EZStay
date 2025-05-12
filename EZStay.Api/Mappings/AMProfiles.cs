using AutoMapper;
using EZStay.Api.Models.Domain;
using EZStay.Api.Models.DTOs;

namespace EZStay.Api.Mappings
{
    public class AMProfiles : Profile
    {
        public AMProfiles()
        {
            // Mapping between User and RegisterDto
            CreateMap<User, RegisterDto>().ReverseMap(); 

            // Mapping between Property and PropertyDto
            CreateMap<Property, PropertyDto>().ReverseMap(); 

            // Mapping between Booking and BookingDto
            CreateMap<Booking, BookingDto>().ReverseMap(); 

            // Mapping between Review and ReviewDto
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
