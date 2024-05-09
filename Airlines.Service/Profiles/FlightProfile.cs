using Airlines.Service.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Service.Profiles;
public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Flight, FlightDto>().ReverseMap();
        CreateMap<Airport, AirportDto>().ReverseMap();
        CreateMap<FlightDto, Flight>().ReverseMap();
        CreateMap<AirportDto, Airport>().ReverseMap();
    }
}