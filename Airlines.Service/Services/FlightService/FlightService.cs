using Airlines.Persistence.Entities;
using Airlines.Persistence.Repository;
using Airlines.Persistence.Repository.Interfaces;
using Airlines.Service.Dto;
using Airlines.Service.Mappers;

namespace Airlines.Service.Services.FlightService;
public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly FlightMapper _mapper;

    public FlightService(IFlightRepository flightRepository, FlightMapper flightMapper)
    {
        _flightRepository = flightRepository;
        _mapper = flightMapper;
    }

    public async Task<FlightDto?> GetFlightByIdAsync(int id)
    {
        var flight = await _flightRepository.GetFlightByIdAsync(id);
        return flight != null ? _mapper.MapFlight(flight) : null;
    }

    public async Task<List<FlightDto>> GetAllFlightsAsync()
    {
        var flights = await _flightRepository.GetAllFlightsAsync();
        return flights.Select(_mapper.MapFlight).ToList();
    }

    public async Task<List<FlightDto>> GetAllFlightsAsync(string filter, string value)
    {


        var flights = await _flightRepository.GetAllFlightsByFilterAsync(filter, value);
        return flights.Select(_mapper.MapFlight).ToList();
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

        return flights.Select(_mapper.MapFlight).ToList(); ;
    }

    public async Task<FlightDto?> AddFlightAsync(FlightDto flightDto)
    {
        var flight = await _flightRepository.AddFlightAsync(_mapper.MapFlight(flightDto));
        return flight != null ? _mapper.MapFlight(flight) : null;
    }

    public async Task<FlightDto?> UpdateFlightAsync(FlightDto updatedFlight)
    {
        var targetFlight = _flightRepository.GetFlightByIdAsync(updatedFlight.FlightId);

        if (targetFlight == null)
        {
            return null;
        }

        var flight = await _flightRepository.UpdateFlightAsync(_mapper.MapFlight(updatedFlight));
        return flight != null ? _mapper.MapFlight(flight) : null;
    }

    public async Task<FlightDto?> UpdateFlightAsync(int id, FlightDto updatedFlight)
    {
        var flight = await _flightRepository.UpdateFlightAsync(id, _mapper.MapFlight(updatedFlight));
        return flight != null ? _mapper.MapFlight(flight) : null;
    }

    public async Task<FlightDto?> DeleteFlightAsync(int id)
    {
        var flight = await _flightRepository.DeleteFlightAsync(id);
        return flight != null ? _mapper.MapFlight(flight) : null;
    }

    public async Task<int> GetFlightsCountAsync() => await _flightRepository.GetFlightsCountAsync();

    public bool IsArrivalDateAfterDeprtureDate(DateTime departureDateTime, DateTime arrivalDateTime) => departureDateTime >= arrivalDateTime;

    public bool IsArrivalDateInTheFuture(DateTime arrivalDateTime) => arrivalDateTime > DateTime.UtcNow;

    public bool IsDepartureDateInTheFuture(DateTime departureDateTime) => departureDateTime > DateTime.UtcNow;
}