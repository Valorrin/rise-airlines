using Airlines.Service.Dto;

namespace Airlines.Service.Services.FlightService;
public interface IFlightService
{
    Task<List<FlightDto>> GetAllFlightsAsync();

    Task<List<FlightDto>> GetAllFlightsAsync(string filter, string value);

    Task<bool> AddFlightAsync(FlightDto flightDto);

    Task<bool> UpdateFlightAsync(int id, FlightDto updatedFlight);

    Task<bool> DeleteFlightAsync(int id);
}