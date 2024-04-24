using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Persistence.Profiles;
public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Flight, FlightDto>().ReverseMap();
        CreateMap<Airport, AirportDto>().ReverseMap();
    }
}