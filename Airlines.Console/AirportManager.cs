using static Airlines.Console.Search;

namespace Airlines.Console;
public class AirportManager
{
    public List<string> Airports { get; private set; }

    public AirportManager() => Airports = [];

    private bool Validate(string name)
    {
        if (LinearSearch(Airports, name) >= 0)
        {
            System.Console.WriteLine($" Error: Airport with the same name already exists.");
            return false;
        }

        if (name.Length != 3)
        {
            System.Console.WriteLine($" Error: Airport name '{name}' must be exactly 3 characters long!");
            return false;
        }

        if (!name.All(char.IsLetter))
        {
            System.Console.WriteLine($" Error: Airport name '{name}' must contain only alphabetic characters!");
            return false;
        }

        return true;

    }

    public void Add(string name)
    {
        if (Validate(name))
        {
            Airports.Add(name);
            System.Console.WriteLine($"Airport '{name}' added successfully.");
        }
    }

    public void Print()
    {
        System.Console.Write($" Airports: ");
        System.Console.WriteLine(string.Join(", ", Airports));

    }
}
