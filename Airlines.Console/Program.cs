using Airlines.Console;
using static Airlines.Console.Search;
using static Airlines.Console.Sorts;
using static Airlines.Console.DataManipulation;
using System;

namespace Airlines;

public class Program
{
    public static void Main()
    {

        var airports = new AirportManager();
        var airlines = new AirlineManager();
        var flights = new FlightManager();

        ReadAirportInput(airports);
        ReadAirlineInput(airlines);
        ReadFlightInput(flights);

        airports.Print();
        airlines.Print();
        airlines.Print();

        airports.Sort(true);
        airlines.Sort(false);
        flights.Sort(false);

        airports.Print();
        airlines.Print();
        flights.Print();
    }

    public static string ReadInput()
    {
        System.Console.WriteLine($"Enter name:");

        var input = System.Console.ReadLine();

        return input;
    }

    public static void ReadAirportInput(AirportManager manager)
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

            manager.Add(input);
        }
    }
    public static void ReadAirlineInput(AirlineManager manager)
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

            manager.Add(input);
        }
    }
    public static void ReadFlightInput(FlightManager manager)
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

            manager.Add(input);
        }
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