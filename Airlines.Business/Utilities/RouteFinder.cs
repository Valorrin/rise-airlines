using Airlines.Business.DataStructures;
using Airlines.Business.Managers;
using Airlines.Business.Models;
using System.Data;

namespace Airlines.Business.Utilities;
public class RouteFinder
{
    private readonly AirportManager _airportManager;

    public RouteFinder(AirportManager airportManager) => _airportManager = airportManager;

    public (List<Flight> route, double totalDuration, double totalPrice, int numStops) FindRoute(Airport startAirport, Airport endAirport, string strategy, FlightRouteGraph routeGraph)
    {
        var weightFunction = _weightFunctions[strategy];
        var graph = routeGraph.AdjacencyList;
        var visitedAirports = new HashSet<Airport>();
        var distances = new Dictionary<Airport, double>();
        var previousFlight = new Dictionary<Airport, Flight>();
        double totalDuration = 0;
        double totalPrice = 0;
        var numStops = 0;

        foreach (var airport in graph.Keys)
        {
            distances[airport] = airport == startAirport ? 0 : double.PositiveInfinity;
        }

        while (visitedAirports.Count < graph.Count)
        {
            var currentAirport = GetNextAirport(distances, visitedAirports);
            _ = visitedAirports.Add(currentAirport);

            foreach (var flight in graph[currentAirport])
            {
                var neighborAirport = _airportManager.GetAirportById(flight.ArrivalAirport);
                var totalDistance = distances[currentAirport] + weightFunction(flight);

                if (totalDistance < distances[neighborAirport])
                {
                    distances[neighborAirport] = totalDistance;
                    previousFlight[neighborAirport] = flight;
                }
            }
        }

        var route = ReconstructRoute(previousFlight, startAirport, endAirport);
        totalDuration = route.Sum(flight => flight.Duration);
        totalPrice = route.Sum(flight => flight.Price);
        numStops = route.Count;

        Console.WriteLine("Route:");
        foreach (var flight in route)
        {
            Console.WriteLine($"Flight {flight.Id}: {flight.DepartureAirport} -> {flight.ArrivalAirport}");
        }
        Console.WriteLine($"Total duration: {totalDuration}");
        Console.WriteLine($"Total price: {totalPrice}");
        Console.WriteLine($"Number of stops: {numStops}");

        return (route, totalDuration, totalPrice, numStops);
    }



    private readonly Dictionary<string, Func<Flight, double>> _weightFunctions = new()
        {
            {"cheap", flight => flight.Price },
            {"short", flight => flight.Duration },
            {"stops", flight => 0}
        };

    private Airport GetNextAirport(Dictionary<Airport, double> distances, HashSet<Airport> visitedAirports)
    {
        var unvisitedAirports = distances.Where(kv => !visitedAirports.Contains(kv.Key));
        var minDistance = double.PositiveInfinity;
        Airport nextAirport = null!;

        foreach (var kvp in unvisitedAirports)
        {
            if (kvp.Value < minDistance)
            {
                minDistance = kvp.Value;
                nextAirport = kvp.Key;
            }
        }

        return nextAirport;
    }

    private List<Flight> ReconstructRoute(Dictionary<Airport, Flight> previousFlight, Airport startAirport, Airport endAirport)
    {
        var route = new List<Flight>();
        var currentAirport = endAirport;

        while (currentAirport != startAirport)
        {
            if (!previousFlight.TryGetValue(currentAirport, out var value))
            {
                return [];
            }

            var flight = value;
            route.Add(flight);
            currentAirport = _airportManager.GetAirportById(flight.DepartureAirport);
        }

        route.Reverse();
        return route;
    }
}