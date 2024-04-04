using Airlines.Business.Models;

namespace Airlines.Business;

public class FlightRouteGraph
{
    public string StartAirportId { get; }

    private readonly Dictionary<string, Queue<string>> _adjacencyList;

    public FlightRouteGraph(string startAirportId)
    {
        StartAirportId = startAirportId;
        _adjacencyList = [];
    }
    public void AddAirport(string airport)
    {
        if (!_adjacencyList.ContainsKey(airport))
        {
            _adjacencyList[airport] = [];
        }
    }

    public void AddFlight(Flight flight)
    {
        var departureAirport = flight.DepartureAirport;
        var arrivalAirport = flight.ArrivalAirport;

        AddAirport(departureAirport);
        AddAirport(arrivalAirport);

        _adjacencyList[departureAirport].Enqueue(arrivalAirport);
    }

    public void RemoveLastFlight(string startAirportId)
    {
        if (_adjacencyList.ContainsKey(startAirportId))
        {
            var outgoingFlights = _adjacencyList[startAirportId];
            if (outgoingFlights.Count > 0)
            {
                _ = outgoingFlights.Dequeue();
            }
            else
            {
                Console.WriteLine("No outgoing flights to remove.");
            }
        }
        else
        {
            Console.WriteLine("Airport not found in the graph.");
        }
    }

    public void PrintGraph()
    {
        foreach (var entry in _adjacencyList)
        {
            var departureAirport = entry.Key;
            var arrivalAirports = entry.Value;
            Console.Write($"Flights from {departureAirport} to: ");
            foreach (var arrivalAirport in arrivalAirports)
            {
                Console.Write($"{arrivalAirport}, ");
            }
            Console.WriteLine();
        }
    }

    public List<string> GetConnectedAirports(string airport)
    {
        if (_adjacencyList.ContainsKey(airport))
            return new List<string>(_adjacencyList[airport]);
        else
            return [];
    }

    public bool IsConnected(string startAirportId, string endAirportId)
    {
        var visited = new HashSet<string>();
        var queue = new Queue<string>();

        visited.Add(startAirportId);
        queue.Enqueue(startAirportId);

        while (queue.Count > 0)
        {
            var currentAirport = queue.Dequeue();

            if (currentAirport == endAirportId)
            {
                return true;
            }

            foreach (var neighbor in _adjacencyList[currentAirport])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return false;
    }
}
