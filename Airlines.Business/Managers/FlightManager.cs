using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class FlightManager
{
    public List<Flight> Flights { get; private set; }

    internal FlightManager() => Flights = [];

    internal void Add(Flight flight) => Flights.Add(flight);

    internal void Add(IList<string> flightData)
    {
        foreach (var flight in flightData)
        {
            var flightParts = flight.Split(", ");

            var newFlight = new Flight
            {
                Id = flightParts[0],
                DepartureAirport = flightParts[1],
                ArrivalAirport = flightParts[2],
                AircraftModel = flightParts[3]
            };

            Add(newFlight);
        }
    }

    internal void Search(string searchTerm)
    {
        var flightIds = Flights.Where(flight => flight.Id == searchTerm).ToList();

        if (flightIds.Count > 0)
            Console.WriteLine($" {searchTerm} is Flight name.");
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