using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IAirportRepository
{
    public Task<Airport?> GetAirportByIdAsync(int id);
    public Task<List<Airport>> GetAllAirportsAsync();
    public Task<List<Airport>> GetAllAirportsByFilterAsync(string filter, string value);
    public Task<Airport?> AddAirportAsync(Airport airport);
    public Task<Airport?> UpdateAirportAsync(int id, Airport airport);
    public Task<Airport?> DeleteAirportAsync(int id);
    public Task<int> GetAirportsCountAsync();
    public Task<bool> IsAirportCodeUniqueAsync(string code);
    public Task<bool> IsAirportNameUniqueAsync(string name);
}