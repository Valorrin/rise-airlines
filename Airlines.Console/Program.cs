using Airlines.Console;
using static Airlines.Console.Validation;
using static Airlines.Console.Search;
using static Airlines.Console.Sort;
using static Airlines.Console.Data;

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

        PrintSearchResult(airports, airlines, flights);
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
}