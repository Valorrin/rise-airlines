using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IAirlineRepository
{
    public Task<List<Airline>> GetAllAirlinesAsync();

    public Task<List<Airline>> GetAllAirlinesByFilterAsync(string filter, string value);

    public Task<bool> AddAirlineAsync(Airline airline);

    public Task<bool> UpdateAirlineAsync(int id, Airline airline);

    public Task<bool> DeleteAirlineAsync(int id);
}