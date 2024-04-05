using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteNewCommand : ICommand
{
    private readonly RouteManager _routeManager;

    private RouteNewCommand(RouteManager routeManager) => _routeManager = routeManager;

    public void Execute() => _routeManager.New();

    public static RouteNewCommand CreateRouteNewCommand(RouteManager routeManager) => new(routeManager);
}