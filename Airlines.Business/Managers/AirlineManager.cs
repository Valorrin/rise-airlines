using Airlines.Business.Models;

namespace Airlines.Business.Managers;
public class AirlineManager
{
    public Dictionary<string, Airline> Airlines { get; private set; }

    public AirlineManager() => Airlines = [];

    public void Add(Airline airline) => Airlines.Add(airline.Id, airline);

    public void Add(IList<string> airlineData)
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

    public void Search(string searchTerm)
    {
        var airlineNames = Airlines.Values.Select(airline => airline.Name).ToList();

        if (airlineNames != null)
            Console.WriteLine($" {searchTerm} is Airline name.");
    }

    public List<string> SortByName()
    {
        var airlineNames = Airlines.Values.Select(airline => airline.Name).ToList().OrderBy(name => name).ToList();

        return airlineNames;
    }

    public List<string> SortDescByName()
    {
        var airlineNames = Airlines.Values.Select(airline => airline.Name).ToList().OrderByDescending(name => name).ToList();

        return airlineNames;
    }
}
