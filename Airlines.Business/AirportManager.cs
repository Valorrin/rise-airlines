using static Airlines.Business.Search;

namespace Airlines.Business;
public class AirportManager
{
    public List<string> Airports { get; private set; }

    public AirportManager() => Airports = [];

    public bool Validate(string name)
    {
        if (LinearSearch(Airports, name) >= 0)
        {
            Console.WriteLine($" Error: Airport with the same name already exists.");
            return false;
        }

        if (name.Length != 3)
        {
            Console.WriteLine($" Error: Airport name '{name}' must be exactly 3 characters long!");
            return false;
        }

        if (!name.All(char.IsLetter))
        {
            Console.WriteLine($" Error: Airport name '{name}' must contain only alphabetic characters!");
            return false;
        }

        return true;

    }

    public void Add(string name)
    {
        Airports.Add(name);
        Console.WriteLine($"Airport '{name}' added successfully.");
    }

    public void Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine(" Error: search term cannot be null or empty!");
        }

        var airportsCopy = Airports.ToList();
        airportsCopy.Sort();

        if (BinarySearch(airportsCopy, searchTerm) >= 0)
        {
            Console.WriteLine($" {searchTerm} is Airport name.");
        }
    }
}
