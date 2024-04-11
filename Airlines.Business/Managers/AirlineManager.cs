using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class AirlineManager
{
    public List<Airline> Airlines { get; private set; }

    public AirlineManager() => Airlines = [];

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
        var airlineNames = Airlines.Where(airline => airline.Name == searchTerm).ToList();

        if (airlineNames.Count > 0)
        {
            Console.WriteLine($" {searchTerm} is Airline name.");
        }
    }

    internal List<string> SortByName()
    {
        var airlineNames = Airlines.Select(airline => airline.Name).OrderBy(name => name).ToList();

        return airlineNames;
    }

    internal List<string> SortDescByName()
    {
        var airlineNames = Airlines.Select(airline => airline.Name).OrderByDescending(name => name).ToList();

        return airlineNames;
    }
}