using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IAirlineRepository
{
    public Task<Airline?> GetAirlineByIdAsync(int id);
    public Task<List<Airline>> GetAllAirlinesAsync();
    public Task<List<Airline>> GetAllAirlinesByFilterAsync(string filter, string value);
    public Task<int> GetAirlinesCountAsync();
    public Task<bool> AddAirlineAsync(Airline airline);
    public Task<bool> UpdateAirlineAsync(int id, Airline airline);
    public Task<bool> DeleteAirlineAsync(int id);
    public Task<bool> IsAirlineNameUniqueAsync(string name);
}