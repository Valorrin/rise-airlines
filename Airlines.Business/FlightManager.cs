
using static Airlines.Console.Search;

namespace Airlines.Console;
public class FlightManager
{
    public List<string> Flights { get; private set; }

    public FlightManager() => Flights = [];

    public bool Validate(string name)
    {
        if (LinearSearch(Flights, name) >= 0)
        {
            System.Console.WriteLine($" Error: Flight with the same name already exists.");
            return false;
        }

        if (!name.All(char.IsLetterOrDigit))
        {
            System.Console.WriteLine($" Error: Flight name '{name}' must contain only alphabetic or numeric characters!");
            return false;
        }

        return true;

    }

    public void Add(string name)
    {
        if (Validate(name))
        {
            Flights.Add(name);
            System.Console.WriteLine($"Flight '{name}' added successfully.");
        }
    }

    public void Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            System.Console.WriteLine(" Error: search term cannot be null or empty!");
        }

        if (BinarySearch(Flights, searchTerm) >= 0)
        {
            System.Console.WriteLine($" {searchTerm} is Flight name.");
        }
    }
}
