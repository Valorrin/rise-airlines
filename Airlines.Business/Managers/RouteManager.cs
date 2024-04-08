using Airlines.Business.DataStructures;
using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class RouteManager
{
    private readonly AirportManager _airportManager;
    public FlightRouteGraph Route { get; private set; }

    internal RouteManager(AirportManager airportManager)
    {
        _airportManager = airportManager;
        Route = new FlightRouteGraph();
    }

    internal void New() => Route = new FlightRouteGraph();

    internal void Add(Flight flight) => AddFlight(flight);

    internal void AddAirport(Airport airport)
    {
        if (!Route.AdjacencyList.ContainsKey(airport))
        {
            Route.AdjacencyList[airport] = [];
        }
    }

    internal void AddFlight(Flight flight)
    {
        var departureAirport = _airportManager.GetAirportById(flight.DepartureAirport);
        var arrivalAirport = _airportManager.GetAirportById(flight.ArrivalAirport);

        AddAirport(departureAirport);
        AddAirport(arrivalAirport);

        Route.AdjacencyList[departureAirport].Add(flight);
    }

    internal void RemoveLastFlight()
    {
        var airports = Route.AdjacencyList.Keys.ToList();

        if (airports.Count == 0)
        {
            Console.WriteLine("No flights to remove.");
            return;
        }

        var lastAirport = airports.Last();

        var flights = Route.AdjacencyList[lastAirport];

        if (flights.Count == 0)
        {
            Console.WriteLine("No flights to remove.");
            return;
        }

        var lastFlight = flights.Last();
        _ = flights.Remove(lastFlight);

        Console.WriteLine($"Removed last flight: {lastFlight.Id}");
    }

    internal bool IsConnected(Airport startAirport, Airport endAirport)
    {
        var visited = new HashSet<Airport>();
        var queue = new Queue<Airport>();

        _ = visited.Add(startAirport);
        queue.Enqueue(startAirport);

        while (queue.Count > 0)
        {
            var currentAirport = queue.Dequeue();

            if (currentAirport == endAirport)
            {
                Console.WriteLine("Connected!");
                return true;
            }

            foreach (var flight in Route.AdjacencyList[currentAirport])
            {
                var neighbor = _airportManager.GetAirportById(flight.ArrivalAirport);
                if (!visited.Contains(neighbor))
                {
                    _ = visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
        Console.WriteLine("Not onnected!");

        return false;
    }

    internal List<Flight> ShortestPath(Airport startAirport, Airport endAirport)
    {
        var previous = new Dictionary<Airport, Flight>();
        var queue = new Queue<Airport>();

        previous[startAirport] = null!;
        queue.Enqueue(startAirport);

        while (queue.Count > 0)
        {
            var currentAirport = queue.Dequeue();

            if (currentAirport == endAirport)
            {
                // Reconstruct path
                var path = new List<Flight>();
                while (currentAirport != null)
                {
                    var previousFlight = previous[currentAirport];
                    if (previousFlight != null)
                        path.Add(previousFlight);
                    currentAirport = _airportManager.GetAirportById(previousFlight?.DepartureAirport!);
                }
                path.Reverse(); // Reverse the path to get correct order

                foreach (var flight in path)
                {
                    Console.WriteLine($" Flight {flight.Id}: {flight.DepartureAirport} -> {flight.ArrivalAirport}");
                }
                return path;
            }

            foreach (var flight in Route.AdjacencyList[currentAirport])
            {
                var neighbor = _airportManager.GetAirportById(flight.ArrivalAirport);
                if (!previous.ContainsKey(neighbor))
                {
                    previous[neighbor] = flight;
                    queue.Enqueue(neighbor);
                }
            }
        }

        // No path found
        return [];
    }

    internal void Print()
    {
        foreach (var entry in Route.AdjacencyList)
        {
            var departureAirport = entry.Key;
            var flights = entry.Value;
            Console.Write($" Flights from {departureAirport.Id} to: ");
            foreach (var flight in flights)
            {
                var arrivalAirport = _airportManager.GetAirportById(flight.ArrivalAirport);
                Console.Write($"{arrivalAirport.Id}");
            }
            Console.WriteLine();
        }
    }

    internal bool Find(string destinationAirportId)
    {
        var destinationAirport = _airportManager.GetAirportById(destinationAirportId);

        if (destinationAirport == null)
        {
            return false;
        }

        var visited = new HashSet<Airport>();
        var queue = new Queue<Airport>();
        var routeFound = false;

        foreach (var airport in Route.AdjacencyList.Keys)
        {
            if (airport.Id == destinationAirportId)
            {
                continue;
            }

            visited.Clear();
            queue.Clear();

            _ = visited.Add(airport);
            queue.Enqueue(airport);

            while (queue.Count > 0)
            {
                var currentAirport = queue.Dequeue();

                if (currentAirport.Id == destinationAirportId)
                {
                    routeFound = true;
                    break;
                }

                foreach (var flight in Route.AdjacencyList[currentAirport])
                {
                    var neighbor = _airportManager.GetAirportById(flight.ArrivalAirport);
                    if (!visited.Contains(neighbor))
                    {
                        _ = visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            if (routeFound)
            {
                Console.WriteLine($"Route found from {airport.Name} to {destinationAirport.Name}");
                break;
            }
        }

        if (!routeFound)
        {
            Console.WriteLine($"No route found to {destinationAirport.Name}");
        }

        return routeFound;
    }

    public List<Flight> FindRoute(Airport startAirport, Airport endAirport, string strategy)
    {
        return strategy switch
        {
            "cheap" => FindCheapestRoute(startAirport, endAirport),
            "short" => FindShortestRoute(startAirport, endAirport),
            "stops" => ShortestPath(startAirport, endAirport),
            _ => throw new ArgumentException("Invalid route search strategy."),
        };
    }

    public List<Flight> FindShortestRoute(Airport startAirport, Airport endAirport)
    {
        return FindRoute(startAirport, endAirport, (flight1, flight2) => flight1.Duration.CompareTo(flight2.Duration));
    }

    public List<Flight> FindCheapestRoute(Airport startAirport, Airport endAirport)
    {
        return FindRoute(startAirport, endAirport, (flight1, flight2) => flight1.Price.CompareTo(flight2?.Price ?? double.MaxValue));
    }

    private List<Flight> FindRoute(Airport startAirport, Airport endAirport, Comparison<Flight> compare)
    {
        var graph = Route.AdjacencyList;
        var visitedAirports = new HashSet<Airport>();
        var distances = new Dictionary<Airport, double>();
        var previousFlight = new Dictionary<Airport, Flight>();

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
                var totalDistance = distances[currentAirport];

                // Handle single edge graph (no flight2)
                if (flight != default)
                {
                    totalDistance += compare(flight, default!);
                }

                if (totalDistance < distances[neighborAirport])
                {
                    distances[neighborAirport] = totalDistance;
                    previousFlight[neighborAirport] = flight!;
                }
            }
        }

        return ReconstructRoute(previousFlight, startAirport, endAirport);
    }

    private Airport GetNextAirport(Dictionary<Airport, double> distances, HashSet<Airport> visitedAirports)
    {
        var unvisitedAirports = distances.Where(kv => !visitedAirports.Contains(kv.Key));
        var minDistance = double.PositiveInfinity;
        Airport nextAirport = null;

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
            if (!previousFlight.ContainsKey(currentAirport))
            {
                // No route exists
                return new List<Flight>();
            }

            var flight = previousFlight[currentAirport];
            route.Add(flight);
            currentAirport = _airportManager.GetAirportById(flight.DepartureAirport);
        }

        route.Reverse();
        return route;
    }
}
