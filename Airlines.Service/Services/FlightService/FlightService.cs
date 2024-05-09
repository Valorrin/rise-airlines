using Airlines.Service.Dto;

namespace Airlines.Service.Services.FlightService;
public class FlightService : IFlightService
{
    public Task<bool> AddFlightAsync(FlightDto flightDto) => throw new NotImplementedException();
    public Task<bool> DeleteFlightAsync(int id) => throw new NotImplementedException();
    public Task<List<FlightDto>> GetAllFlightsAsync() => throw new NotImplementedException();
    public Task<List<FlightDto>> GetAllFlightsAsync(string filter, string value) => throw new NotImplementedException();
    public Task<bool> UpdateFlightAsync(int id, FlightDto updatedFlight) => throw new NotImplementedException();
}
