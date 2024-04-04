using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class RouteManager
{
    public Dictionary<string, FlightRouteGraph> Routes { get; set; }

    public RouteManager() => Routes = [];

    internal void Add(FlightRouteGraph route) => Routes.Add(route.StartAirportId, route);

    internal void Add(List<string> routeData, FlightManager flightManager)
    {
        var startAirportId = routeData[0];
        var newRouteGraph = new FlightRouteGraph(startAirportId);
        newRouteGraph.AddAirport(startAirportId);

        for (var i = 1; i < routeData.Count; i++)
        {
            var flight = flightManager.GetFlightById(routeData[i]);
            newRouteGraph.AddFlight(flight);
        }

        Add(newRouteGraph);
    }

    internal void AddFlight(Flight flight, string startAirportId) => Routes[startAirportId].AddFlight(flight);

    internal void RemoveFlight(string startAirportId) => Routes[startAirportId].RemoveLastFlight(startAirportId);

    internal void Find(string startAirportId, string destinationAirportId) => Console.WriteLine("Not implemented");

    internal void Print(string startAirportId) => Routes[startAirportId].PrintGraph();
}