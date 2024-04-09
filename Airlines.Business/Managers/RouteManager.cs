﻿using Airlines.Business.DataStructures;
using Airlines.Business.Models;
using Airlines.Business.Utilities;

namespace Airlines.Business.Managers;
public class RouteManager
{
    private readonly AirportManager _airportManager;
    private readonly RouteFinder _routeFinder;

    public FlightRouteGraph Route { get; private set; }

    public RouteManager(AirportManager airportManager)
    {
        _airportManager = airportManager;
        _routeFinder = new RouteFinder(airportManager);
        Route = new FlightRouteGraph();
    }

    internal void New() => Route = new FlightRouteGraph();

    internal void Add(Flight flight) => AddFlight(flight);

    internal void AddAirport(Airport airport)
    {
        if (!Route.AdjacencyList.ContainsKey(airport))
        {
            Route.AdjacencyList[airport] = new List<Flight>();
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
        Console.WriteLine("Not connected!");

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