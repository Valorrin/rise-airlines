using static Airlines.Console.Search;

namespace Airlines.Console;
public class AirlineManager
{
    public List<string> Airlines { get; private set; }

    public AirlineManager() => Airlines = [];

    private bool Validate(string name)
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

    public void Print()
    {
        System.Console.Write($" Airlines: ");
        System.Console.WriteLine(string.Join(", ", Airlines));

    }
}
