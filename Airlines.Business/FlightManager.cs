using static Airlines.Business.Search;

namespace Airlines.Business;
public class FlightManager
{
    public LinkedList<Flight> Flights { get; private set; }

    public FlightManager() => Flights = [];

    public void Add(Flight flight) => Flights.AddLast(flight);

    public void Add(List<string> flightData)
    {
        foreach (var flight in flightData)
        {
            var newFlight = new Flight();
            var flightParts = flight.Split(", ");
            newFlight.Id = flightParts[0];
            newFlight.DepartureAirport = flightParts[1];
            newFlight.ArrivalAirport = flightParts[2];

            Add(newFlight);
        }
    }

    public bool Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine(" Error: search term cannot be null or empty!");
            return false;
        }

        var flightIds = Flights.Select(flight => flight.Id).ToList().OrderBy(name => name).ToList(); ;

        return BinarySearch(flightIds, searchTerm) >= 0;
    }
}
