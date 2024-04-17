using Airlines.Business.Models;

namespace Airlines.Business.DataStructures;

public class FlightRouteGraph
{
    public Dictionary<Airport, List<Flight>> AdjacencyList { get; set; }

    public FlightRouteGraph() => AdjacencyList = [];
}
