using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteAddCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly FlightManager _flightManager;
    private readonly string _flightId;

    private RouteAddCommand(RouteManager routeManager, FlightManager flightManager, string flightId)
    {
        _routeManager = routeManager;
        _flightManager = flightManager;
        _flightId = flightId;
    }

    public void Execute()
    {
        var flightToAdd = _flightManager.Flights.FirstOrDefault(x => x.Id == _flightId);

        if (flightToAdd != null && _routeManager.Validate(flightToAdd))
        {
            _routeManager.AddFlight(flightToAdd);
            Console.WriteLine($"Flight with ID '{_flightId}' added to the route.");
        }
        else
            Console.WriteLine($"Error: Flight '{_flightId}' does not exist or cannot be added.");
    }

    public static RouteAddCommand CreateRouteAddCommand(RouteManager routeManager, FlightManager flightManager, string flightId) => new(routeManager, flightManager, flightId);

}