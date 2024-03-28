using Airlines.Business.Models;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using static Airlines.Business.Utilities.Search;

namespace Airlines.Business.Managers;
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
            AirportsByCity[airport.City] = [];
        _ = AirportsByCity[airport.City].Add(airport);

        if (!AirportsByCountry.ContainsKey(airport.Country))
            AirportsByCountry[airport.Country] = [];
        _ = AirportsByCountry[airport.Country].Add(airport);

        _ = AirportNames.Add(airport.Name);
        Airports.Add(airport.Id, airport);
    }

    public void Add(IList<string> airportData)
    {
        foreach (var airport in airportData)
        {
            var airportParts = airport.Split(", ");

            var newAirport = new Airport
            {
                Id = airportParts[0],
                Name = airportParts[1],
                City = airportParts[2],
                Country = airportParts[3]
            };

            Add(newAirport);
        }
    }

    public void Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
            Console.WriteLine(" Error: search term cannot be null or empty!");

        var airportNames = Airports.Values.Select(airline => airline.Name).ToList().OrderBy(name => name).ToList(); ;

        if (BinarySearch(airportNames, searchTerm) >= 0)
            Console.WriteLine($" {searchTerm} is Airport name.");
    }

    public bool Exist(string name) => AirportNames.Contains(name);

    public void ListData(string name, string airportsFrom)
    {
        if (airportsFrom == "City")
        {
            var names = AirportsByCity[name];

            foreach (var airport in names)
                Console.WriteLine(airport.Name);
        }
        else if (airportsFrom == "Country")
        {
            var names = AirportsByCountry[name];

            foreach (var airport in names)
                Console.WriteLine(airport.Name);
        }
    }
}