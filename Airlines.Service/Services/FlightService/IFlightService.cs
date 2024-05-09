using Airlines.Service.Dto;

namespace Airlines.Service.Services.FlightService;
public interface IFlightService
{
    public void GetAllFlights();

    public void PrintAllFlights(string filter, string value);

    public void AddFlight(FlightDto flightDto);

    public void UpdateFlight(int id, FlightDto updatedFlight);

    public void DeleteFlight(int id);
}