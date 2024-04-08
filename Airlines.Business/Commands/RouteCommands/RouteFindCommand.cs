using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteFindCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly Airport _destinationAirport;

    private RouteFindCommand(RouteManager routeManager, Airport destinationAirport)
    {
        _routeManager = routeManager;
        _destinationAirport = destinationAirport;
    }

    public void Execute() => _routeManager.Find(_destinationAirport.Id);

    public static RouteFindCommand CreateRouteFindCommand(RouteManager routeManager, Airport destinationAirport) => new(routeManager, destinationAirport);
}