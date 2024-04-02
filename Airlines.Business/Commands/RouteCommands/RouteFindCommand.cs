using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteFindCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly string _startAirportId;

    private RouteFindCommand(RouteManager routeManager, string startAirportId)
    {
        _routeManager = routeManager;
        _startAirportId = startAirportId;
    }

    public void Execute() => _routeManager.Print(_startAirportId);

    public static RouteFindCommand CreateRouteFindCommand(RouteManager routeManager, string startAirportId) => new(routeManager, startAirportId);
}