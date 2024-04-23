using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Persistence.Profiles;
public class AirlineProfile : Profile
{
    public AirlineProfile() => CreateMap<Airline, AirlineDto>().ReverseMap();
}