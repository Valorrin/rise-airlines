using Airlines.Service.Dto;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Airlines.Service.Services.FlightService;
public interface IFlightService
{
    Task<List<FlightDto>> GetAllFlightsAsync();

    Task<List<FlightDto>> GetAllFlightsAsync(string filter, string value);

    Task<int> GetFlightsCountAsync();

    Task<bool> AddFlightAsync(FlightDto flightDto);

    Task<bool> UpdateFlightAsync(int id, FlightDto updatedFlight);

    Task<bool> DeleteFlightAsync(int id);
}