using Airlines.Business.DataStructures;
using Airlines.Business.Models;

using Airlines.Business.Utilities;
using System.Text;

namespace Airlines.Business.Managers;
public class RouteManager
{
    private readonly AirportManager _airportManager;
    private readonly RouteFinder _routeFinder;
    private readonly ILogger _logger;

    public FlightRouteGraph Route { get; private set; }

    public RouteManager(AirportManager airportManager, ILogger logger)
    {
        _airportManager = airportManager;
        _routeFinder = new RouteFinder(airportManager, logger);
        _logger = logger;
        Route = new FlightRouteGraph();
    }

    internal void New() => Route = new FlightRouteGraph();

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
            _logger.Log("No flights to remove.");
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

                _logger.Log($"Removed last flight ({lastFlight.Id}) from {airport.Name}");
                return;
            }
        }

        _logger.Log("No flights to remove.");
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
                _logger.Log("Connected!");
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
        _logger.Log("Not connected!");

        return false;
    }

    internal void Print()
    {
        foreach (var entry in Route.AdjacencyList)
        {
            var departureAirport = entry.Key;
            var flights = entry.Value;
            var flightDetails = new StringBuilder();
            _ = flightDetails.Append($" Flights from {departureAirport.Id} to: ");
            foreach (var flight in flights)
            {
                var arrivalAirport = _airportManager.GetAirportById(flight.ArrivalAirport);
                _ = flightDetails.Append($"{arrivalAirport.Id}");
            }

            _logger.Log(flightDetails.ToString());
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
                _logger.Log($"Route found from {airport.Name} to {destinationAirport.Name}");
                break;
            }
        }

        if (!routeFound)
        {
            _logger.Log($"No route found to {destinationAirport.Name}");
        }

        return routeFound;
    }

    public (List<Flight> route, double totalDuration, double totalPrice, int numStops) FindRoute(Airport startAirport, Airport endAirport, string strategy)
    {
        if (!IsConnected(startAirport, endAirport))
        {
            throw new ArgumentException("No route exists between the start and end airports.");
        }

        var routeGraph = Route;
        return _routeFinder.FindRoute(startAirport, endAirport, strategy, routeGraph);
    }
}