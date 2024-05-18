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

    public async Task<AirportDto?> GetAirportByIdAsync(int id)
    {
        var airport = await _airportRepository.GetAirportByIdAsync(id);
        return airport != null ? _mapper.MapAirport(airport) : null;
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

    public async Task<AirportDto?> AddAirportAsync(AirportDto airportDto)
    {

        var airport = await _airportRepository.AddAirportAsync(_mapper.MapAirport(airportDto));
        return airport != null ? _mapper.MapAirport(airport) : null;
    }

    public async Task<AirportDto?> UpdateAirportAsync(AirportDto updatedAirport)
    {
        var targetAirport = _airportRepository.GetAirportByIdAsync(updatedAirport.AirportId);

        if (targetAirport == null)
        {
            return null;
        }

        var airport = await _airportRepository.UpdateAirportAsync(_mapper.MapAirport(updatedAirport));
        return airport != null ? _mapper.MapAirport(airport) : null;
    }

    public async Task<AirportDto?> UpdateAirportAsync(int id, AirportDto updatedAirport)
    {
        var airport = await _airportRepository.UpdateAirportAsync(id, _mapper.MapAirport(updatedAirport));
        return airport != null ? _mapper.MapAirport(airport) : null;
    }

    public async Task<AirportDto?> DeleteAirportAsync(int id)
    {
        var airport = await _airportRepository.DeleteAirportAsync(id);
        return airport != null ? _mapper.MapAirport(airport) : null;
    }

    public async Task<int> GetAirportsCountAsync() => await _airportRepository.GetAirportsCountAsync();

    public async Task<bool> IsAirportCodeUniqueAsync(string code) => code != null && await _airportRepository.IsAirportCodeUniqueAsync(code);

    public async Task<bool> IsAirportNameUniqueAsync(string name) => name != null && await _airportRepository.IsAirportNameUniqueAsync(name);

    public bool IsAirportCodeLengthValid(string? code) => code != null && code.Length <= 3;
}