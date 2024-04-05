using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteFindCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly string _destinationAirportId;

    private RouteFindCommand(RouteManager routeManager, string destinationAirportId)
    {
        _routeManager = routeManager;
        _destinationAirportId = destinationAirportId;
    }

    public void Execute() => _routeManager.Find(_destinationAirportId);

    public static RouteFindCommand CreateRouteFindCommand(RouteManager routeManager, string destinationAirportId) => new(routeManager, destinationAirportId);
}