using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteFindCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly string _startAirportId;
    private readonly string _destinationAirportId;

    private RouteFindCommand(RouteManager routeManager, string startAirportId, string destinationAirportId)
    {
        _routeManager = routeManager;
        _startAirportId = startAirportId;
        _destinationAirportId = destinationAirportId;
    }

    public void Execute() => _routeManager.Find(_startAirportId, _destinationAirportId);

    public static RouteFindCommand CreateRouteFindCommand(RouteManager routeManager, string startAirportId, string destinationAirportId) => new(routeManager, startAirportId, destinationAirportId);
}