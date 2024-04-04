using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteNewCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly FlightRouteGraph _route;

    private RouteNewCommand(RouteManager routeManager, FlightRouteGraph route)
    {
        _routeManager = routeManager;
        _route = route;
    }

    public void Execute() => _routeManager.Add(_route);

    public static RouteNewCommand CreateRouteNewCommand(RouteManager routeManager, FlightRouteGraph route) => new(routeManager, route);
}