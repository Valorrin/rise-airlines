using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteFindCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly Airport _destinationAirport;

    public RouteFindCommand(RouteManager routeManager, Airport destinationAirport)
    {
        _routeManager = routeManager;
        _destinationAirport = destinationAirport;
    }

    public void Execute() => _routeManager.Find(_destinationAirport.Id);
}