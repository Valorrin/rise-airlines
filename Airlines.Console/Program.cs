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

        airports.ReadInput();
        airlines.ReadInput();
        flights.ReadInput();

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