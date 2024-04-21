using Airlines.Persistence.Dto;
using Airlines.Persistence.Entities;
using AutoMapper;

namespace Airlines.Persistence.Profiles;
internal class AirlineMapper
{
    private readonly IMapper _mapper;

    public AirlineMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Airline, AirlineDto>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }
    public Airline MapAirline(AirlineDto airlineDto)
    {
        var airline = _mapper.Map<Airline>(airlineDto);

        return airline;
    }
}
