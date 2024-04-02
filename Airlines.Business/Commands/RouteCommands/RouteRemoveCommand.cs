using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteRemoveCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly string _startAirportId;

    private RouteRemoveCommand(RouteManager routeManager, string startAirportId)
    {
        _routeManager = routeManager;
        _startAirportId = startAirportId;
    }

    public void Execute() => _routeManager.RemoveFlight(_startAirportId);

    public static RouteRemoveCommand CreateRouteRemoveCommand(RouteManager routeManager, string startAirportId) => new(routeManager, startAirportId);
}