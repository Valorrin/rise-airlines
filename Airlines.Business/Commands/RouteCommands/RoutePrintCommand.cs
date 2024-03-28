using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RoutePrintCommand : ICommand
{
    private readonly RouteManager _routeManager;

    private RoutePrintCommand(RouteManager routeManager) => _routeManager = routeManager;

    public void Execute() => _routeManager.Print();

    public static RoutePrintCommand CreateRoutePrintCommand(RouteManager routeManager) => new(routeManager);

}