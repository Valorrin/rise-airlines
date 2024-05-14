using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository.Interfaces;
using Airlines.Service.Dto;
using Airlines.Service.Mappers;
using Airlines.Service.Services.AirportService;

namespace Airlines.Service.Services.FlightService;
public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly FlightMapper _flightMapper;

    public FlightService(IFlightRepository flightRepository, FlightMapper flightMapper)
    {
        _flightRepository = flightRepository;
        _flightMapper = flightMapper;
    }

    public async Task<List<FlightDto>> GetAllFlightsAsync()
    {
        var flights = await _flightRepository.GetAllFlightsAsync();
        return flights.Select(_flightMapper.MapFlight).ToList();
    }
    public async Task<List<FlightDto>> GetAllFlightsAsync(string filter, string value)
    {


        var flights = await _flightRepository.GetAllFlightsByFilterAsync(filter, value);
        return flights.Select(_flightMapper.MapFlight).ToList();
    }
    public async Task<List<FlightDto>> GetAllFlightForTimePeriod(string timePeriod)
    {
        List<Flight> flights = [];

        if (timePeriod == "day")
        {
            flights = await _flightRepository.GetAllFlightsForTodayAsync();
        }

        else if (timePeriod == "week")
        {
            flights = await _flightRepository.GetAllFlightsForThisWeekAsync();
        }

        else if (timePeriod == "month")
        {
            flights = await _flightRepository.GetAllFlightsForThisMonthAsync();
        }

        return flights.Select(_flightMapper.MapFlight).ToList(); ;
    }
    public async Task<int> GetFlightsCountAsync() => await _flightRepository.GetFlightsCountAsync();
    public async Task<bool> AddFlightAsync(FlightDto flightDto) => await _flightRepository.AddFlightAsync(_flightMapper.MapFlight(flightDto));
    public async Task<bool> UpdateFlightAsync(int id, FlightDto updatedFlight) => await _flightRepository.UpdateFlightAsync(id, _flightMapper.MapFlight(updatedFlight));
    public async Task<bool> DeleteFlightAsync(int id) => await _flightRepository.DeleteFlightAsync(id);
    public bool IsArrivalDateAfterDeprtureDate(DateTime departureDateTime, DateTime arrivalDateTime) => departureDateTime >= arrivalDateTime;
    public bool IsArrivalDateInTheFuture(DateTime arrivalDateTime) => arrivalDateTime > DateTime.UtcNow;
    public bool IsDepartureDateInTheFuture(DateTime departureDateTime) => departureDateTime > DateTime.UtcNow;
}