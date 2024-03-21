﻿using static Airlines.Business.Search;

namespace Airlines.Business;
public class AirportManager
{
    public Dictionary<string, Airport> Airports { get; private set; }
    public Dictionary<string, HashSet<Airport>> AirportsByCity { get; private set; }
    public Dictionary<string, HashSet<Airport>> AirportsByCountry { get; private set; }
    public HashSet<string> AirportNames { get; private set; }

    public AirportManager()
    {
        Airports = [];
        AirportsByCity = [];
        AirportsByCountry = [];
        AirportNames = [];
    }

    public bool IsIdUnique(string id)
    {
        if (Airports.ContainsKey(id))
        {
            Console.WriteLine(" Error: An airport with the same ID already exists.");
            return false;
        }

        return true;
    }
    public void Add(Airport airport)
    {
        if (!IsIdUnique(airport.Id))
            return;

        if (!AirportsByCity.ContainsKey(airport.City))
            AirportsByCity[airport.City] = new HashSet<Airport>();
        AirportsByCity[airport.City].Add(airport);

        if (!AirportsByCountry.ContainsKey(airport.Country))
            AirportsByCountry[airport.Country] = new HashSet<Airport>();
        AirportsByCountry[airport.Country].Add(airport);

        AirportNames.Add(airport.Name);
        Airports.Add(airport.Id, airport);

        Console.WriteLine($"Airport '{airport.Name}' added successfully.");
    }

    public void Add(List<string> airportData)
    {
        foreach (var airport in airportData)
        {
            var newAirport = new Airport();
            var airportParts = airport.Split(", ");
            newAirport.Id = airportParts[0];
            newAirport.Name = airportParts[1];
            newAirport.City = airportParts[2];
            newAirport.Country = airportParts[3];

            if (IsIdUnique(newAirport.Id))
            {
                Add(newAirport);
            }
        }
    }

    public void Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine(" Error: search term cannot be null or empty!");
        }

        var airportNames = Airports.Values.Select(airline => airline.Name).ToList();
        airportNames = airportNames.OrderBy(name => name).ToList();

        if (BinarySearch(airportNames, searchTerm) >= 0)
        {
            Console.WriteLine($" {searchTerm} is Airport name.");
        }
    }

    public bool Exist(string name) => AirportNames.Contains(name);

    public void ListData(string name, string airportsFrom)
    {
        if (airportsFrom == "City")
        {
            var names = AirportsByCity[name];

            foreach (var airport in names)
            {
                Console.WriteLine(airport.Name);
            }
        }
        else if (airportsFrom == "Country")
        {
            var names = AirportsByCountry[name];

            foreach (var airport in names)
            {
                Console.WriteLine(airport.Name);
            }
        }
    }
}
