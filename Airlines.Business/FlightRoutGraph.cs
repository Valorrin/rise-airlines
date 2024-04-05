using Airlines.Business.Models;

namespace Airlines.Business;

public class FlightRouteGraph
{
    public Dictionary<Airport, List<Flight>> AdjacencyList { get; set; }

    public FlightRouteGraph() => AdjacencyList = [];

}
