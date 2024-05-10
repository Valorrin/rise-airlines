using Airlines.Persistence.Repository;
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

    public async Task<List<AirportDto>> GetAllAirportsAsync()
    {
        var airports = await _airportRepository.GetAllAirportsAsync();
        return airports.Select(_mapper.MapAirport).ToList();
    }
    public async Task<List<AirportDto>> GetAllAirportsAsync(string filter, string value)
    {
        var airports = await _airportRepository.GetAllAirportsByFilterAsync(filter, value);
        return airports.Select(_mapper.MapAirport).ToList();
    }
    public async Task<bool> AddAirportAsync(AirportDto airporteDto) => await _airportRepository.AddAirportAsync(_mapper.MapAirport(airporteDto));
    public async Task<bool> UpdateAirportAsync(string id, AirportDto updatedAirport) => await _airportRepository.UpdateAirportAsync(id, _mapper.MapAirport(updatedAirport));
    public async Task<bool> DeleteAirportAsync(string id) => await _airportRepository.DeleteAirportAsync(id);
}