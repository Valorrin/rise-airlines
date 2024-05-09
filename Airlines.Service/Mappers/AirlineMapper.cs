using Airlines.Service.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Service.Mappers;
public class AirlineMapper
{
    private readonly IMapper _mapper;

    public AirlineMapper(IMapper mapper) => _mapper = mapper;
    public Airline MapAirline(AirlineDto airlineDto)
    {
        var airline = _mapper.Map<Airline>(airlineDto);

        return airline;
    }
}