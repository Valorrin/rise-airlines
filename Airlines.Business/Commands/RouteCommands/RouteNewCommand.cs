using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteNewCommand : ICommand
{
    private readonly RouteManager _routeManager;

    public RouteNewCommand(RouteManager routeManager) => _routeManager = routeManager;

    public void Execute() => _routeManager.Routes.Clear();
}
