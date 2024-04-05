using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class RouteManager
{
    private readonly AirportManager _airportManager;
    public FlightRouteGraph Route { get; private set; }


    public RouteManager(AirportManager airportManager)
    {
        _airportManager = airportManager;
        Route = new FlightRouteGraph();
    }

    public void New() => Route = new FlightRouteGraph();

    public void Add(List<string> routeData, AirportManager airportManager, FlightManager flightManager)
    {
        var airport = airportManager.GetAirportById(routeData[0]);

        AddAirport(airport);

        for (var i = 1; i < routeData.Count; i++)
        {
            var flight = flightManager.GetFlightById(routeData[i]);
            AddFlight(flight);
        }
    }

    public void AddAirport(Airport airport)
    {
        if (!Route.AdjacencyList.ContainsKey(airport))
        {
            Route.AdjacencyList[airport] = [];
        }
    }

    public void AddFlight(Flight flight)
    {
        var departureAirport = _airportManager.GetAirportById(flight.DepartureAirport);
        var arrivalAirport = _airportManager.GetAirportById(flight.ArrivalAirport);

        AddAirport(departureAirport);
        AddAirport(arrivalAirport);

        Route.AdjacencyList[departureAirport].Add(flight);
    }

    public void RemoveLastFlight()
    {
        // Get all airports in the graph
        var airports = Route.AdjacencyList.Keys.ToList();

        // Check if there are any airports in the graph
        if (airports.Count == 0)
        {
            Console.WriteLine("No flights to remove.");
            return;
        }

        // Get the last airport in the list
        var lastAirport = airports.Last();

        // Get the flights for the last airport
        var flights = Route.AdjacencyList[lastAirport];

        // Check if there are any flights for the last airport
        if (flights.Count == 0)
        {
            Console.WriteLine("No flights to remove.");
            return;
        }

        // Remove the last flight from the flights list
        var lastFlight = flights.Last();
        _ = flights.Remove(lastFlight);

        Console.WriteLine($"Removed last flight: {lastFlight.Id}");
    }

    public bool IsConnected(Airport startAirport, Airport endAirport)
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

        return false;
    }

    public List<Flight> ShortestPath(Airport startAirport, Airport endAirport)
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

    public void Print()
    {
        foreach (var entry in Route.AdjacencyList)
        {
            var departureAirport = entry.Key;
            var flights = entry.Value;
            Console.Write($"Flights from {departureAirport.Name} to: ");
            foreach (var flight in flights)
            {
                var airportName = _airportManager.GetAirportById(flight.ArrivalAirport).Name;
                Console.Write($"{airportName}, ");
            }
            Console.WriteLine();
        }
    }
    public bool Find(string destinationAirportId)
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
                // Skip if the current airport is the destination airport
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
}