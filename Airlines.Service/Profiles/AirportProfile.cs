using Airlines.Service.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Service.Profiles;
public class AirportProfile : Profile
{
    public AirportProfile()
    {
        CreateMap<Airport, AirportDto>().ReverseMap();
        CreateMap<AirportDto, Airport>().ReverseMap();
    }
}