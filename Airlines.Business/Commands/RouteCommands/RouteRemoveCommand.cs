using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteRemoveCommand : ICommand
{
    private readonly RouteManager _routeManager;

    private RouteRemoveCommand(RouteManager routeManager) => _routeManager = routeManager;

    public void Execute() => _routeManager.RemoveLastFlight();

    public static RouteRemoveCommand CreateRouteRemoveCommand(RouteManager routeManager) => new(routeManager);
}