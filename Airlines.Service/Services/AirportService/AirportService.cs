using Airlines.Persistence.Repository.Interfaces;
using Airlines.Service.Dto;
using Airlines.Service.Mappers;

namespace Airlines.Service.Services.AirportService;
public class AirportService : IAirportService
{
    private readonly IAirportRepository _airportRepository;
    private readonly AirportMapper _mapper;
    public AirportService(IAirportRepository airportRepository, AirportMapper mapper)
    {
        _airportRepository = airportRepository;
        _mapper = mapper;
    }

    public Task<List<AirlineDto>> GetAllAirportsAsync() => throw new NotImplementedException();
    public Task<List<AirlineDto>> GetAllAirportsAsync(string filter, string value) => throw new NotImplementedException();
    public Task<bool> AddAirportAsync(AirportDto airporteDto) => throw new NotImplementedException();
    public Task<bool> DeleteAirportAsync(int id) => throw new NotImplementedException();
    public Task<bool> UpdateAirportAsync(int id, AirportDto updatedAirport) => throw new NotImplementedException();
}