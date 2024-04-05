using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteSearchCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly Airport _startAirport;
    private readonly Airport _endAirport;


    private RouteSearchCommand(RouteManager routeManager, Airport startAirport, Airport endAirport)
    {
        _routeManager = routeManager;
        _startAirport = startAirport;
        _endAirport = endAirport;
    }

    public void Execute() => _routeManager.ShortestPath(_startAirport, _endAirport);

    public static RouteSearchCommand CreateRouteSearchCommand(RouteManager routeManager, Airport startAirport, Airport endAirport) => new(routeManager, startAirport, endAirport);
}