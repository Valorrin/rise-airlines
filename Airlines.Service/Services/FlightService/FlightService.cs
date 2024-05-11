using Airlines.Persistence.Repository.Interfaces;
using Airlines.Service.Dto;
using Airlines.Service.Mappers;

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
    public async Task<int> GetFlightsCountAsync() => await _flightRepository.GetFlightsCountAsync();
    public async Task<bool> AddFlightAsync(FlightDto flightDto) => await _flightRepository.AddFlightAsync(_flightMapper.MapFlight(flightDto));
    public async Task<bool> UpdateFlightAsync(int id, FlightDto updatedFlight) => await _flightRepository.UpdateFlightAsync(id, _flightMapper.MapFlight(updatedFlight));
    public async Task<bool> DeleteFlightAsync(int id) => await _flightRepository.DeleteFlightAsync(id);
}