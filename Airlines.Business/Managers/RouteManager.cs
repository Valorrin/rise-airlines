using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class RouteManager
{
    public Dictionary<string, FlightRouteTree> Routes { get; set; }

    public RouteManager() => Routes = [];

    public void Add(FlightRouteTree route) => Routes.Add(route.Root.Airport, route);

    public void Add(List<string> routeData, FlightManager flightManager)
    {
        var newRouteTree = new FlightRouteTree(routeData[0]);

        for (var i = 1; i < routeData.Count; i++)
        {
            var flight = flightManager.GetFlightById(routeData[i]);
            newRouteTree.AddFlight(flight);

        }
        Add(newRouteTree);
    }

    public void AddFlight(Flight flight, string startAirportId) => Routes[startAirportId].AddFlight(flight);

    public void RemoveFlight(string startAirportId) => Routes[startAirportId].RemoveLastFlight();

    public void Print(string startAirportId) => Routes[startAirportId].PrintRoute();
}