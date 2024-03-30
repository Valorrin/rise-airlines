using Airlines.Business.Models;
using static Airlines.Business.Utilities.Search;

namespace Airlines.Business.Managers;
public class FlightManager
{
    public List<Flight> Flights { get; private set; }

    public FlightManager() => Flights = [];

    public void Add(Flight flight) => Flights.Add(flight);

    public void Add(IList<string> flightData)
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

    public void Search(string searchTerm)
    {
        var flightIds = Flights.Select(flight => flight.Id).ToList().OrderBy(name => name).ToList(); ;

        if (BinarySearch(flightIds, searchTerm) >= 0)
            Console.WriteLine($" {searchTerm} is Flight name.");
    }

    public string GetAircraftModel(string flightId)
    {
        var aircraftModel = Flights.Where(f => f.Id == flightId).Select(f => f.AircraftModel).FirstOrDefault();
        return aircraftModel ?? throw new Exception("Flight not found");
    }
}