using Airlines.Service.Dto;

namespace Airlines.Service.Services.AirlineService;

public interface IAirlineService
{
    Task<List<AirlineDto>> GetAllAirlinesAsync();

    Task<List<AirlineDto>> GetAllAirlinesAsync(string filter, string value);

    Task<int> GetAirlinesCountAsync();

    Task<bool> AddAirlineAsync(AirlineDto airlineDto);

    Task<bool> UpdateAirlineAsync(int id, AirlineDto updatedAirline);

    Task<bool> DeleteAirlineAsync(int id);

    public bool IsAirportNameLenghtValid(string? name);
}