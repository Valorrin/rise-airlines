using Airlines.Console;

namespace Airlines;

public class Program
{
    public static void Main()
    {
        var airports = new string[10000];
        var airlines = new string[10000];
        var flights = new string[10000];

        airports = ReadInput("Airport", airports);
        airlines = ReadInput("Airline", airlines);
        flights = ReadInput("Flight", flights);

        PrintData("Airport", airports);
        PrintData("Airline", airlines);
        PrintData("Flight", flights);

        _ = airports.SortAirports(true);
        _ = airlines.SortAirlines(false);
        _ = flights.SortFlights(false);

        PrintData("Airport", airports);
        PrintData("Airline", airlines);
        PrintData("Flight", flights);

        Search(airports, airlines, flights);
    }

    public static string[] ReadInput(string type, string[] currentData)
    {
        var newData = currentData;

        System.Console.WriteLine($"Enter {type} name, or type 'done' to finish:\n");

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

            if (type == "Airport")
            {
                if (ValidateAirport(input, newData))
                {
                    newData = AddData(input, newData);
                }
            }
            else if (type == "Airline")
            {
                if (ValidateAirline(input, newData))
                {
                    newData = AddData(input, newData);
                }
            }
            else if (type == "Flight")
            {

                if (ValidateFlight(input, newData))
                {
                    newData = AddData(input, newData);
                }
            }
        }

        return newData;
    }

    public static bool Validate(string? value, string[] values)
    {
        if (string.IsNullOrEmpty(value))
        {
            System.Console.WriteLine(" Error: Name cannot be null or empty!");
            return false;
        }

        if (LinearSearch(values, value) >= 0)
        {
            System.Console.WriteLine($" Error: The name already exist!");
            return false;
        }

        return true;
    }

    public static bool ValidateAirport(string airport, string[] airports)
    {
        if (!Validate(airport, airports))
        {
            return false;
        }

        if (airport.Length != 3)
        {
            System.Console.WriteLine($" Error: Airport name '{airport}' must be exactly 3 characters long!");
            return false;
        }

        if (!airport.All(char.IsLetter))
        {
            System.Console.WriteLine($" Error: Airport name '{airport}' must contain only alphabetic characters!");
            return false;
        }

        return true;
    }

    public static bool ValidateAirline(string airline, string[] airlines)
    {
        if (!Validate(airline, airlines))
        {
            return false;
        };

        if (airline.Length >= 6)
        {
            System.Console.WriteLine($" Error: Airline name '{airline}' must be less than 6 characters long!");
            return false;
        }

        return true;
    }

    public static bool ValidateFlight(string flight, string[] flights)
    {
        if (!Validate(flight, flights))
        {
            return false;
        }

        if (!flight.All(char.IsLetterOrDigit))
        {
            System.Console.WriteLine($" Error: Flight code '{flight}' must contain only alphabetic or numeric characters!");
            return false;
        }

        return true;
    }

    public static string[] AddData(string item, string[] data)
    {

        for (var i = 0; i < data.Length; i++)
        {
            if (data[i] == null)
            {
                data[i] = item;

                break;
            }
        }

        System.Console.WriteLine($" {item} was added successfully!");

        return data;
    }

    public static void PrintData(string type, string[] data)
    {
        System.Console.Write($" {type}s: ");

        for (var i = 0; i < data.Length; i++)
        {
            if (data[i] != null)
            {
                System.Console.Write($" {data[i]} ");
            }
        }
        System.Console.WriteLine();
    }

    public static int LinearSearch(string[] array, string target)
    {
        for (var i = 0; i < array.Length; i++)
        {
            if (array[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    public static int BinarySearch(string[] arr, string target)
    {
        int l = 0, r = arr.Length - 1;
        while (l <= r)
        {
            var m = l + ((r - l) / 2);

            var comparisonResult = string.Compare(arr[m], target);

            if (comparisonResult == 0)
                return m;

            if (comparisonResult < 0)
                l = m + 1;
            else
                r = m - 1;
        }

        return -1;
    }

    public static void Search(string[] airports, string[] airlines, string[] flights)
    {
        System.Console.WriteLine($"\nEnter search term or type 'done' to finish:\n");

        while (true)
        {
            var searchTerm = System.Console.ReadLine();

            var termFound = false;

            if (string.IsNullOrEmpty(searchTerm))
            {
                System.Console.WriteLine(" Error: search term cannot be null or empty!");
                continue;
            }

            if (searchTerm == "done")
            {
                System.Console.WriteLine();
                break;
            }

            if (BinarySearch(airports, searchTerm) >= 0)
            {
                termFound = true;
                System.Console.WriteLine($" {searchTerm} is Airport name.");
            }

            if (BinarySearch(airlines, searchTerm) >= 0)
            {
                termFound = true;
                System.Console.WriteLine($" {searchTerm} is Airline name.");
            }

            if (BinarySearch(flights, searchTerm) >= 0)
            {
                termFound = true;
                System.Console.WriteLine($" {searchTerm} is Flight name.");
            }

            if (!termFound)
            {
                System.Console.WriteLine($" {searchTerm} was not found.");
            }
        }
    }
}