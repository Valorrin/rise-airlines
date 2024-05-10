using Airlines.Service.Dto;

namespace Airlines.Service.Services.AirportService;
public interface IAirportService
{
    Task<List<AirportDto>> GetAllAirportsAsync();

    Task<List<AirportDto>> GetAllAirportsAsync(string filter, string value);

    Task<bool> AddAirportAsync(AirportDto airporteDto);

    Task<bool> UpdateAirportAsync(string id, AirportDto updatedAirport);

    Task<bool> DeleteAirportAsync(string id);
}