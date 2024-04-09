using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteCheckCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly Airport _startAirport;
    private readonly Airport _endAirport;


    public RouteCheckCommand(RouteManager routeManager, Airport startAirport, Airport endAirport)
    {
        _routeManager = routeManager;
        _startAirport = startAirport;
        _endAirport = endAirport;
    }

    public void Execute() => _routeManager.IsConnected(_startAirport, _endAirport);
}