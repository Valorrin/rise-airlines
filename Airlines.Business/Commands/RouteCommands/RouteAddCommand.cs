using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteAddCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly Flight _flight;


    public RouteAddCommand(RouteManager routeManager, Flight flight)
    {
        _routeManager = routeManager;
        _flight = flight;
    }

    public void Execute() => _routeManager.AddFlight(_flight);
}