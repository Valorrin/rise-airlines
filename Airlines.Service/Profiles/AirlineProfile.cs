using Airlines.Service.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Service.Profiles;
public class AirlineProfile : Profile
{
    public AirlineProfile()
    {
        CreateMap<Airline, AirlineDto>().ReverseMap();
        CreateMap<AirlineDto, Airline>().ReverseMap();
    }
}