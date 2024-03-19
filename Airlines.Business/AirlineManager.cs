using static Airlines.Console.Search;

namespace Airlines.Console;
public class AirlineManager
{
    public List<string> Airlines { get; set; }

    public AirlineManager() => Airlines = [];

    public bool Validate(string name)
    {
        if (LinearSearch(Airlines, name) >= 0)
        {
            System.Console.WriteLine($" Error: Airline with the same name already exists.");
            return false;
        }

        if (name.Length >= 6)
        {
            System.Console.WriteLine($" Error: Airline name '{name}' must be less than 6 characters long!");
            return false;
        }

        return true;

    }

    public void Add(string name)
    {
        if (Validate(name))
        {
            Airlines.Add(name);
            System.Console.WriteLine($"Airline '{name}' added successfully.");
        }
    }

    public void Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            System.Console.WriteLine(" Error: search term cannot be null or empty!");
        }

        var airlinesCopy = Airlines.ToList();
        airlinesCopy.Sort();

        if (BinarySearch(airlinesCopy, searchTerm) >= 0)
        {
            System.Console.WriteLine($" {searchTerm} is Airline name.");
        }
    }
}
