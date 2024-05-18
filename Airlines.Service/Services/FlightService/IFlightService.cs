using Airlines.Service.Dto;

namespace Airlines.Service.Services.FlightService;
public interface IFlightService
{
    Task<FlightDto?> GetFlightByIdAsync(int id);
    Task<List<FlightDto>> GetAllFlightsAsync();
    Task<List<FlightDto>> GetAllFlightsAsync(string filter, string value);
    Task<List<FlightDto>> GetAllFlightForTimePeriod(string timePeriod);
    Task<FlightDto?> AddFlightAsync(FlightDto flightDto);
    Task<FlightDto?> UpdateFlightAsync(FlightDto updatedFlight);
    Task<FlightDto?> UpdateFlightAsync(int id, FlightDto updatedFlight);
    Task<FlightDto?> DeleteFlightAsync(int id);
    Task<int> GetFlightsCountAsync();
    bool IsArrivalDateInTheFuture(DateTime arrivalDateTime);
    bool IsDepartureDateInTheFuture(DateTime departureDateTime);
    bool IsArrivalDateAfterDeprtureDate(DateTime departureDateTime, DateTime arrivalDateTime);
}