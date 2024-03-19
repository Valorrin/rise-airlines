using static Airlines.Business.Search;

namespace Airlines.Business;
public class FlightManager
{
    public List<string> Flights { get; private set; }

    public FlightManager() => Flights = [];

    public bool Validate(string name)
    {
        if (LinearSearch(Flights, name) >= 0)
        {
            Console.WriteLine($" Error: Flight with the same name already exists.");
            return false;
        }

        if (!name.All(char.IsLetterOrDigit))
        {
            Console.WriteLine($" Error: Flight name '{name}' must contain only alphabetic or numeric characters!");
            return false;
        }

        return true;

    }

    public void Add(string name)
    {
        if (Validate(name))
        {
            Flights.Add(name);
            Console.WriteLine($"Flight '{name}' added successfully.");
        }
    }

    public void Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine(" Error: search term cannot be null or empty!");
        }

        if (BinarySearch(Flights, searchTerm) >= 0)
        {
            Console.WriteLine($" {searchTerm} is Flight name.");
        }
    }
}
