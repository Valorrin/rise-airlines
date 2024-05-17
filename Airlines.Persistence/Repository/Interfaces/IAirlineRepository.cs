using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IAirlineRepository
{
    public Task<Airline?> GetAirlineByIdAsync(int id);
    public Task<List<Airline>> GetAllAirlinesAsync();
    public Task<List<Airline>> GetAllAirlinesByFilterAsync(string filter, string value);
    public Task<Airline?> AddAirlineAsync(Airline airline);
    public Task<Airline?> UpdateAirlineAsync(Airline airline);
    public Task<Airline?> UpdateAirlineAsync(int id, Airline airline);
    public Task<Airline?> DeleteAirlineAsync(int id);
    public Task<int> GetAirlinesCountAsync();
    public Task<bool> IsAirlineNameUniqueAsync(string name);
}