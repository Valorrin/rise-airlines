using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Persistence.Profiles;
public class AirportProfile : Profile
{
    public AirportProfile() => CreateMap<Airport, AirportDto>().ReverseMap();
}