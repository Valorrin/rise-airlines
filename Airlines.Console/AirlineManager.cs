using static Airlines.Console.Search;

namespace Airlines.Console;
public class AirlineManager
{
    public List<string> Airlines { get; private set; }

    public AirlineManager() => Airlines = [];

    public void ReadInput()
    {
        System.Console.WriteLine($"Enter airline name, or type 'done' to finish:\n");

        while (true)
        {
            var input = System.Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                System.Console.WriteLine(" Error: The name cannot be null or empty!");
                continue;
            }

            if (input == "done")
            {
                System.Console.WriteLine();
                break;
            }

            Add(input);
        }
    }

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

    public void Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            System.Console.WriteLine(" Error: search term cannot be null or empty!");
        }

        if (BinarySearch(Airlines, searchTerm) >= 0)
        {
            System.Console.WriteLine($" {searchTerm} is Airport name.");
        }

        else
        {
            System.Console.WriteLine($" {searchTerm} was not found.");
        }
    }

    public void Print()
    {
        System.Console.Write($" Airlines: ");
        System.Console.WriteLine(string.Join(", ", Airlines));

    }
}
