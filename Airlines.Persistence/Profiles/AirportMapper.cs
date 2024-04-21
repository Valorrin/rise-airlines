using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Persistence.Profiles;
public class AirportMapper
{
    private readonly IMapper _mapper;

    public AirportMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Airport, AirportDto>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }
    public Airport MapAirport(AirportDto airportDto)
    {
        var airport = _mapper.Map<Airport>(airportDto);

        return airport;

    }
}