using Airlines.Business.Models;
using Airlines.Business.Utilities;

namespace Airlines.Business.Managers;
public class AirlineManager
{
    private readonly ILogger _logger;

    public List<Airline> Airlines { get; private set; }

    public AirlineManager(ILogger logger)
    {
        Airlines = [];
        _logger = logger;
    }

    internal void Add(Airline airline) => Airlines.Add(airline);

    internal void Add(IList<string> airlineData)
    {
        foreach (var airport in airlineData)
        {
            var airportParts = airport.Split(", ");

            var newAirline = new Airline
            {
                Id = airportParts[0],
                Name = airportParts[1]
            };

            Add(newAirline);
        }
    }

    internal void Search(string searchTerm)
    {
        if (Airlines.Any(airline => airline.Name == searchTerm))
        {
            _logger.Log($" {searchTerm} is Airline name.");
        }
    }

    internal void SortByName()
    {
        Airlines = Airlines.OrderBy(name => name).ToList();
        _logger.Log($"Airlines sorted by name ascending.");
    }

    internal void SortDescByName()
    {
        Airlines = Airlines.OrderByDescending(name => name).ToList();
        _logger.Log($"Airlines sorted by name descending.");
    }
}