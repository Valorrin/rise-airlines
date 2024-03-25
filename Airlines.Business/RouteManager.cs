namespace Airlines.Business;
public class RouteManager
{
    public LinkedList<Flight> Routes { get; set; }

    public RouteManager() => Routes = new LinkedList<Flight>();

    public void AddFlight(Flight flight) => Routes.AddLast(flight);

    public void RemoveFlight() => Routes.RemoveLast();

    public bool Validate(Flight flight)
    {
        if (Routes.Count == 0)
        {
            return true;
        }

        if (Routes.Last!.Value.ArrivalAirport == flight.DepartureAirport)
        {
            return true;
        }

        Console.WriteLine(" ERROR: The DepartureAirport of the new flight doesn't matches the ArrivalAirport of the last flight in the route!");
        return false;
    }

    public bool IsEmpty() => Routes.Count == 0;
}