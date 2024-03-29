using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class RouteManager
{
    public LinkedList<Flight> Routes { get; set; }

    public RouteManager() => Routes = new LinkedList<Flight>();

    public void AddFlight(Flight flight) => Routes.AddLast(flight);

    public void RemoveFlight() => Routes.RemoveLast();

    public bool Validate(Flight flight)
    {
        if (Routes.Count == 0)
            return true;

        if (Routes.Last!.Value.ArrivalAirport == flight.DepartureAirport)
            return true;

        Console.WriteLine(" ERROR: The DepartureAirport of the new flight doesn't matches the ArrivalAirport of the last flight in the route!");
        return false;
    }

    public bool IsEmpty() => Routes.Count == 0;

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