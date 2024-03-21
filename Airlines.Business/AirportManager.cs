using static Airlines.Business.Search;

namespace Airlines.Business;
public class AirportManager
{
    public Dictionary<string, Airport> Airports { get; private set; }

    public AirportManager() => Airports = [];

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
        Airports.Add(airport.Id, airport);

        Console.WriteLine($"Airport '{airport.Name}' added successfully.");
    }

    public void Add(List<string> airportData)
    {
        var newAirport = new Airport();

        foreach (var airport in airportData)
        {
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
}
