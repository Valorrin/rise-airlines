using Airlines.Service.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Service.Mappers;
public class FlightMapper
{
    private readonly IMapper _mapper;

    public FlightMapper(IMapper mapper) => _mapper = mapper;
    public Flight MapFlight(FlightDto flightDto)
    {
        var flight = _mapper.Map<Flight>(flightDto);

        return flight;
    }

    public FlightDto MapFlight(Flight flight)
    {
        var flightDto = _mapper.Map<FlightDto>(flight);

        return flightDto;
    }
}