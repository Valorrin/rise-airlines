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

        for (var i = airports.Count - 1; i >= 0; i--)
        {
            var airport = airports[i];
            var flights = Route.AdjacencyList[airport];

            if (flights.Count > 0)
            {
                var lastFlight = flights.Last();
                _ = flights.Remove(lastFlight);

                Console.WriteLine($"Removed last flight ({lastFlight.Id}) from {airport.Name}");
                return;
            }
        }

        Console.WriteLine("No flights to remove.");
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

    private readonly Dictionary<string, Func<Flight, double>> _weightFunctions = new()
    {
        {"cheap", flight => flight.Price },
        {"short", flight => flight.Duration },
        {"stops", flight => 0}
    };

    public (List<Flight> route, double totalDuration, double totalPrice, int numStops) FindRoute(Airport startAirport, Airport endAirport, string strategy)
    {
        if (!IsConnected(startAirport, endAirport))
        {
            throw new ArgumentException("No route exists between the start and end airports.");
        }

        var weightFunction = _weightFunctions[strategy];
        var graph = Route.AdjacencyList;
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