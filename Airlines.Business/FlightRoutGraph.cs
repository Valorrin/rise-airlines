using Airlines.Business.Models;
using System;
using System.Collections.Generic;

namespace Airlines.Business
{
    public class FlightRouteGraph
    {
        private readonly Dictionary<string, List<string>> _adjacencyList;

        public FlightRouteGraph() => _adjacencyList = [];

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

            _adjacencyList[departureAirport].Add(arrivalAirport);
        }

        public void RemoveFlight(Flight flight)
        {
            var departureAirport = flight.DepartureAirport;
            var arrivalAirport = flight.ArrivalAirport;

            _ = _adjacencyList[departureAirport].Remove(arrivalAirport);
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
                return _adjacencyList[airport];
            else
                return [];
        }
    }
}
