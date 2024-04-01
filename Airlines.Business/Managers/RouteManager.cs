using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class RouteManager
{
    public LinkedList<Flight> Routes { get; set; }

    public RouteManager() => Routes = new LinkedList<Flight>();

    public void AddFlight(Flight flight) => Routes.AddLast(flight);

    public void RemoveFlight() => Routes.RemoveLast();

    public void Print()
    {
        foreach (var flight in Routes)
        {
            Console.WriteLine($"Flight ID: {flight.Id}");
            Console.WriteLine($"Departure Airport ID: {flight.DepartureAirport}");
            Console.WriteLine($"Arrival Airport ID: {flight.ArrivalAirport}");
            Console.WriteLine($"Aircraft Model: {flight.AircraftModel}\n");
        }
    }
}