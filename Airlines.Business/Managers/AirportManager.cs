using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class AirportManager
{
    public List<Airport> Airports { get; private set; }
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

    internal void Add(Airport airport)
    {
        if (!AirportsByCity.ContainsKey(airport.City))
            AirportsByCity[airport.City] = [];
        _ = AirportsByCity[airport.City].Add(airport);

        if (!AirportsByCountry.ContainsKey(airport.Country))
            AirportsByCountry[airport.Country] = [];
        _ = AirportsByCountry[airport.Country].Add(airport);

        _ = AirportNames.Add(airport.Name);
        Airports.Add(airport);
    }

    internal void Search(string searchTerm)
    {
        var airportNames = Airports.Where(airline => airline.Name == searchTerm).ToList();

        if (airportNames.Count > 0)
            Console.WriteLine($" {searchTerm} is Airport name.");
    }

    internal List<string> SortByName()
    {
        var airportNames = Airports.Select(airline => airline.Name).ToList().OrderBy(name => name).ToList();

        return airportNames;
    }

    internal List<string> SortDescByName()
    {
        var airportNames = Airports.Select(airline => airline.Name).ToList().OrderByDescending(name => name).ToList();

        return airportNames;
    }

    internal bool Exist(string name) => AirportNames.Contains(name);

    internal void ListData(string name, string airportsFrom)
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

    internal Airport GetAirportById(string airportId)
    {

        var airport = Airports.FirstOrDefault(a => a.Id == airportId);
        return airport!;
    }
}