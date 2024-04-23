using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Persistence.Mappers;
public class AirportMapper
{
    private readonly IMapper _mapper;

    public AirportMapper(IMapper mapper) => _mapper = mapper;
    public Airport MapAirport(AirportDto airportDto)
    {
        var airport = _mapper.Map<Airport>(airportDto);

        return airport;
    }
}