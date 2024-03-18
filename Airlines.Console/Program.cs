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
        var airports = new List<string>();
        var airlines = new List<string>();
        var flights = new List<string>();

        airports = ReadInput("Airport", airports);
        airlines = ReadInput("Airline", airlines);
        flights = ReadInput("Flight", flights);

        PrintData("Airport", airports);
        PrintData("Airline", airlines);
        PrintData("Flight", flights);

        airports.SortAirports(true);
        airlines.SortAirlines(false);
        flights.SortFlights(false);

        PrintData("Airport", airports);
        PrintData("Airline", airlines);
        PrintData("Flight", flights);

        PrintSearchResult(airports, airlines, flights);
    }

    public static List<string> ReadInput(string type, List<string> currentData)
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