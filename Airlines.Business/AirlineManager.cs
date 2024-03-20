using static Airlines.Business.Search;

namespace Airlines.Business;
public class AirlineManager
{
    public List<string> Airlines { get; private set; }

    public AirlineManager() => Airlines = [];

    public bool Validate(string name)
    {
        if (LinearSearch(Airlines, name) >= 0)
        {
            Console.WriteLine($" Error: Airline with the same name already exists.");
            return false;
        }

        if (name.Length >= 6)
        {
            Console.WriteLine($" Error: Airline name '{name}' must be less than 6 characters long!");
            return false;
        }

        return true;

    }

    public void Add(string name)
    {
        Airlines.Add(name);
        Console.WriteLine($"Airline '{name}' added successfully.");
    }

    public void Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine(" Error: search term cannot be null or empty!");
        }

        var airlinesCopy = Airlines.ToList();
        airlinesCopy.Sort();

        if (BinarySearch(airlinesCopy, searchTerm) >= 0)
        {
            Console.WriteLine($" {searchTerm} is Airline name.");
        }
    }
}
