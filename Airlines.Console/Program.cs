using Airlines.Console;
using static Airlines.Console.Validation;
using static Airlines.Console.Search;
using static Airlines.Console.Sort;
using static Airlines.Console.DataManipulation;
using System;

namespace Airlines;

public class Program
{
    public static void Main()
    {
        var airports = new List<string>();
        var airlines = new List<string>();
        var flights = new List<string>();

        airports = ReadAirportInput(airports);
        airlines = ReadAirlineInput(airlines);
        flights = ReadFlightInput(flights);

        PrintData("Airport", airports);
        PrintData("Airline", airlines);
        PrintData("Flight", flights);

        airports.SortAirports(true);
        airlines.SortAirlines(false);
        flights.SortFlights(false);

        PrintData("Airport", airports);
        PrintData("Airline", airlines);
        PrintData("Flight", flights);
    }

    public static List<string> ReadAirportInput(List<string> data)
    {
        System.Console.WriteLine($"Enter airport name, or type 'done' to finish:\n");

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

            if (ValidateAirport(input, data))
            {
                data = AddData(input, data);
            }
        }

        return data;
    }
    public static List<string> ReadAirlineInput(List<string> data)
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

            if (ValidateAirline(input, data))
            {
                data = AddData(input, data);
            }
        }

        return data;
    }

    public static List<string> ReadFlightInput(List<string> data)
    {
        System.Console.WriteLine($"Enter flight name, or type 'done' to finish:\n");

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

            if (ValidateAirport(input, data))
            {
                data = AddData(input, data);
            }
        }

        return data;
    }

    public static void ReadCommands()
    {
        System.Console.WriteLine($"Enter command:\n");

        while (true)
        {
            var command = System.Console.ReadLine();

            if (string.IsNullOrEmpty(command))
            {
                System.Console.WriteLine(" Error: The command cannot be null or empty!");
                continue;
            }

            string searchTerm = "aaa";

            if (command == "search")
            {            
            }
        }
    }
}