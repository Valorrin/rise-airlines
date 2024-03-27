using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteRemoveCommand : ICommand
{
    private readonly RouteManager _routeManager;

    private RouteRemoveCommand(RouteManager routeManager) => _routeManager = routeManager;

    public void Execute()
    {
        if (!_routeManager.IsEmpty())
        {
            _routeManager.RemoveFlight();
            Console.WriteLine("Last flight removed from the route.");
        }
        else
            Console.WriteLine("Route is already empty.");
    }

    public static RouteRemoveCommand CreateRouteRemoveCommand(RouteManager routeManager) => new(routeManager);

}