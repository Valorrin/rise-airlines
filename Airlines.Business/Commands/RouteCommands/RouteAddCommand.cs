using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Business.Commands.RouteCommands;
public class RouteAddCommand : ICommand
{
    private readonly RouteManager _routeManager;
    private readonly Flight _flight;
    private readonly string _startAirportId;


    private RouteAddCommand(RouteManager routeManager, Flight flight, string startAirportId)
    {
        _routeManager = routeManager;
        _flight = flight;
        _startAirportId = startAirportId;
    }

    public void Execute() => _routeManager.AddFlight(_flight, _startAirportId);

    public static RouteAddCommand CreateRouteAddCommand(RouteManager routeManager, Flight flight, string startAirportId) => new(routeManager, flight, startAirportId);
}