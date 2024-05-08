using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IAirportRepository
{
    public Task<List<Airport>> GetAllAirportsAsync();

    public Task<List<Airport>> GetAllAirportsByFilterAsync(string filter, string value);

    public Task<bool> AddAirportAsync(Airport airport);

    public Task<bool> UpdateAirportAsync(string id, Airport airport);

    public Task<bool> DeleteAirportAsync(string id);
}