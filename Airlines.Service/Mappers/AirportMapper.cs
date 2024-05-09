using Airlines.Service.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Service.Mappers;
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