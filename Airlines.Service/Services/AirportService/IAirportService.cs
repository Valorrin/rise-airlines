using Airlines.Service.Dto;

namespace Airlines.Service.Services.AirportService;
public interface IAirportService
{
    Task<AirportDto?> GetAirportByIdAsync(int id);
    Task<List<AirportDto>> GetAllAirportsAsync();
    Task<List<AirportDto>> GetAllAirportsAsync(string filter, string value);
    Task<int> GetAirportsCountAsync();
    Task<bool> AddAirportAsync(AirportDto airporteDto);
    Task<bool> UpdateAirportAsync(int id, AirportDto updatedAirport);
    Task<bool> DeleteAirportAsync(int id);
    Task<bool> IsAirportCodeUniqueAsync(string code);
    Task<bool> IsAirportNameUniqueAsync(string name);
    bool IsAirportCodeLengthValid(string? code);
}