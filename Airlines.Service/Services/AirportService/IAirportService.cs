using Airlines.Service.Dto;

namespace Airlines.Service.Services.AirportService;
public interface IAirportService
{
    Task<AirportDto?> GetAirportByIdAsync(int id);
    Task<List<AirportDto>> GetAllAirportsAsync();
    Task<List<AirportDto>> GetAllAirportsAsync(string filter, string value);
    Task<AirportDto?> AddAirportAsync(AirportDto airportDto);
    Task<AirportDto?> UpdateAirportAsync(AirportDto updatedAirport);
    Task<AirportDto?> UpdateAirportAsync(int id, AirportDto updatedAirport);
    Task<AirportDto?> DeleteAirportAsync(int id);
    Task<int> GetAirportsCountAsync();
    Task<bool> IsAirportCodeUniqueAsync(string code);
    Task<bool> IsAirportNameUniqueAsync(string name);
    bool IsAirportCodeLengthValid(string? code);
}