using Airlines.Business.Models;
using Airlines.Business.Utilities;

namespace Airlines.Business.Managers;
public class FlightManager
{
    private readonly ILogger _logger;

    public List<Flight> Flights { get; private set; }

    internal FlightManager(ILogger logger)
    {
        Flights = [];
        _logger = logger;
    }

    internal void Add(Flight flight) => Flights.Add(flight);

    internal void Search(string searchTerm)
    {
        if (Flights.Any(flight => flight.Id == searchTerm))
        {
            _logger.Log($"{searchTerm} is Flight name.");
        }
    }

    internal List<string> SortById()
    {
        var flightIds = Flights.Select(flight => flight.Id).ToList().OrderBy(name => name).ToList();

        return flightIds;
    }

    internal List<string> SortDescById()
    {
        var flightIds = Flights.Select(flight => flight.Id).ToList().OrderByDescending(name => name).ToList();

        return flightIds;
    }

    internal string GetAircraftModel(string flightId)
    {
        var aircraftModel = Flights.Where(f => f.Id == flightId).Select(f => f.AircraftModel).FirstOrDefault();
        return aircraftModel!;
    }

    internal Flight GetFlightById(string flightId)
    {

        var flight = Flights.FirstOrDefault(f => f.Id == flightId);
        return flight!;
    }
}