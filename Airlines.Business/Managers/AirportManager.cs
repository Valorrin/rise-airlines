using Airlines.Business.Models;
using Airlines.Business.Utilities;

namespace Airlines.Business.Managers;
public class AirportManager
{
    private readonly ILogger _logger;
    public List<Airport> Airports { get; private set; }

    public AirportManager(ILogger logger)
    {
        Airports = [];
        _logger = logger;
    }

    internal void Add(Airport airport) => Airports.Add(airport);

    internal void Search(string searchTerm)
    {
        if (Airports.Any(airport => airport.Name == searchTerm))
        {
            _logger.Log($" {searchTerm} is Airport name.");
        }
    }

    internal void SortByName()
    {
        Airports = Airports.OrderBy(name => name).ToList();
        _logger.Log($"Airports sorted by name ascending.");
    }

    internal void SortDescByName()
    {
        Airports = Airports.OrderByDescending(name => name).ToList();
        _logger.Log($"Airlines sorted by name descending.");
    }

    internal void Exist(string name)
    {
        if (Airports.Any(x => x.Name == name))
        {
            _logger.Log($"{name} exists.");
        }
        else
        {
            _logger.Log($"{name} does not exist.");
        }
    }

    internal void ListData(string name, string airportsFrom)
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

        foreach (var airport in airports)
        {
            _logger.Log(string.Join(", ", airport.Name));
        }
    }

    internal Airport GetAirportById(string airportId)
    {
        var airport = Airports.FirstOrDefault(a => a.Id == airportId);
        return airport!;
    }
}