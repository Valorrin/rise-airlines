using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteSearchCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly Airport _startAirport;
    private readonly Airport _endAirport;
    private readonly string _strategy;

    public RouteSearchCommand(RouteManager routeManager, Airport startAirport, Airport endAirport, string strategy)
    {
        _routeManager = routeManager;
        _startAirport = startAirport;
        _endAirport = endAirport;
        _strategy = strategy;
    }

    public void Execute() => _routeManager.FindRoute(_startAirport, _endAirport, _strategy);
}