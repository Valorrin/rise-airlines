using static Airlines.Business.Search;

namespace Airlines.Business;
public class AirlineManager
{
    public Dictionary<string, Airline> Airlines { get; private set; }

    public AirlineManager() => Airlines = [];

    public bool IsIdUnique(string id)
    {
        if (Airlines.ContainsKey(id))
        {
            Console.WriteLine(" Error: An airline with the same ID already exists.");
            return false;
        }

        return true;
    }

    public void Add(Airline airline) => Airlines.Add(airline.Id, airline);

    public void Add(List<string> airlineData)
    {
        foreach (var airport in airlineData)
        {
            var newAirline = new Airline();
            var airportParts = airport.Split(", ");
            newAirline.Id = airportParts[0];
            newAirline.Name = airportParts[1];

            if (IsIdUnique(newAirline.Id))
            {
                Add(newAirline);
            }
        }
    }

    public void Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine(" Error: search term cannot be null or empty!");
        }

        var airlineNames = Airlines.Values.Select(airline => airline.Name).ToList().OrderBy(name => name).ToList();

        if (BinarySearch(airlineNames, searchTerm) >= 0)
        {
            Console.WriteLine($" {searchTerm} is Airline name.");
        }
    }
}
