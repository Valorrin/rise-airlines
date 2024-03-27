using Airlines.Business.Managers;

namespace Airlines.Business.Commands;
public class PrintRouteCommand : ICommand
{
    private readonly RouteManager _routeManager;

    private PrintRouteCommand(RouteManager routeManager) => _routeManager = routeManager;

    public void Execute()
    {
        if (_routeManager.Routes == null)
        {
            Console.WriteLine("Route is empty.");
        }
        else
        {
            foreach (var flight in _routeManager.Routes)
            {
                Console.WriteLine($"Flight ID: {flight.Id}");
                Console.WriteLine($"Departure Airport ID: {flight.DepartureAirport}");
                Console.WriteLine($"Arrival Airport ID: {flight.ArrivalAirport}");
                Console.WriteLine($"Aircraft Model: {flight.AircraftModel}\n");
            }
        }
    }

    public static PrintRouteCommand CreatePrintRouteCommand(RouteManager routeManager) => new(routeManager);

}