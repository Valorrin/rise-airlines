using static Airlines.Console.Search;

namespace Airlines.Console;
public static class Validation
{
    public static bool Validate(string? value, List<string> values)
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

    public static bool ValidateAirport(string airport, List<string> airports)
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

    public static bool ValidateAirline(string airline, List<string> airlines)
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

    public static bool ValidateFlight(string flight, List<string> flights)
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
}
