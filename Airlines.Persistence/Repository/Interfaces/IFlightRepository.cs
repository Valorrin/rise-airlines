using Airlines.Persistence.Entities;

namespace Airlines.Persistence.Repository.Interfaces;
public interface IFlightRepository
{
    public Task<Flight?> GetFlightByIdAsync(int id);
    public Task<List<Flight>> GetAllFlightsAsync();
    public Task<List<Flight>> GetAllFlightsByFilterAsync(string filter, string value);
    public Task<List<Flight>> GetAllFlightsForTodayAsync();
    public Task<List<Flight>> GetAllFlightsForThisWeekAsync();
    public Task<List<Flight>> GetAllFlightsForThisMonthAsync();
    public Task<Flight?> AddFlightAsync(Flight flight);
    public Task<Flight?> UpdateFlightAsync(int id, Flight flight);
    public Task<Flight?> DeleteFlightAsync(int id);
    public Task<int> GetFlightsCountAsync();
}