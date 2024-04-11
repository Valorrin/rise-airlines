using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class AirportManager
{
    public List<Airport> Airports { get; private set; }

    public AirportManager() => Airports = [];

    internal void Add(Airport airport) => Airports.Add(airport);

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

    internal bool Exist(string name) => Airports.Any(x => x.Name == name);

    internal List<Airport> ListData(string name, string airportsFrom)
    {
        var airports = new List<Airport>();

        if (airportsFrom == "City")
        {
            airports = Airports.Where(x => x.City == name).ToList();
        }
        else if (airportsFrom == "Country")
        {
            airports = Airports.Where(x => x.Country == name).ToList();
        }

        return airports;
    }

    internal Airport GetAirportById(string airportId)
    {

        var airport = Airports.FirstOrDefault(a => a.Id == airportId);
        return airport!;
    }
}