using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Persistence.Profiles;
public class FlightMapper
{
    private readonly IMapper _mapper;

    public FlightMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Flight, FlightDto>().ReverseMap();
            cfg.CreateMap<Airport, AirportDto>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }
    public Flight MapFlight(FlightDto flightDto)
    {
        var flight = _mapper.Map<Flight>(flightDto);

        return flight;

    }
}