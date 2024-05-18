using Airlines.Service.Dto;

namespace Airlines.Service.Services.AirlineService;

public interface IAirlineService
{
    Task<AirlineDto?> GetAirlineByIdAsync(int id);
    Task<List<AirlineDto>> GetAllAirlinesAsync();
    Task<List<AirlineDto>> GetAllAirlinesAsync(string filter, string value);
    Task<AirlineDto?> AddAirlineAsync(AirlineDto airlineDto);
    Task<AirlineDto?> UpdateAirlineAsync(AirlineDto updatedAirline);
    Task<AirlineDto?> UpdateAirlineAsync(int id, AirlineDto updatedAirline);
    Task<AirlineDto?> DeleteAirlineAsync(int id);
    Task<int> GetAirlinesCountAsync();
    Task<bool> IsAirlineNameUniqueAsync(string name);
    public bool IsAirlineNameLengthValid(string? name);
}