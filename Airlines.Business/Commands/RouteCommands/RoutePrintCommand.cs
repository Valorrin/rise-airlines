using Airlines.Business.Managers;

namespace Airlines.Business.Commands.RouteCommands;
public class RoutePrintCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly string _startAirportId;

    private RoutePrintCommand(RouteManager routeManager, string startAirportId)
    {
        _routeManager = routeManager;
        _startAirportId = startAirportId;
    }

    public void Execute() => _routeManager.Print(_startAirportId);

    public static RoutePrintCommand CreateRoutePrintCommand(RouteManager routeManager, string startAirportId) => new(routeManager, startAirportId);
}