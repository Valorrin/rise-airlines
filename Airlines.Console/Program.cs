using Airlines.Console;
using static Airlines.Console.Sorts;

namespace Airlines;

public class Program
{
    public static void Main()
    {
        var airports = new AirportManager();
        var airlines = new AirlineManager();
        var flights = new FlightManager();

        ReadInput(airports);
        ReadInput(airlines);
        ReadInput(flights);

        Print(airports);
        Print(airlines);
        Print(flights);

        ReadCommands(airports, airlines, flights);
    }

    public static void ReadInput(AirlineManager manager)
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
    public static void ReadInput(AirportManager manager)
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
    public static void ReadInput(FlightManager manager)
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
    public static void Print(AirlineManager manager)
    {
        System.Console.Write($" Airlines: ");
        System.Console.WriteLine(string.Join(", ", manager.Airlines));
    }
    public static void Print(AirportManager manager)
    {
        System.Console.Write($" Airports: ");
        System.Console.WriteLine(string.Join(", ", manager.Airports));

    }
    public static void Print(FlightManager manager)
    {
        System.Console.Write($" Flights: ");
        System.Console.WriteLine(string.Join(", ", manager.Flights));

    }
    public static void ReadCommands(AirportManager airports, AirlineManager airlines, FlightManager flights)
    {
        System.Console.WriteLine($"Enter command:\n");

        while (true)
        {
            var input = System.Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                System.Console.WriteLine(" Error: The input cannot be null or empty!");
                continue;
            }

            var splittedInput = input.Split().ToArray();
            var command = splittedInput[0];


            if (command == "search")
            {
                var searchTerm = splittedInput[1];

                airports.Search(searchTerm);
                airlines.Search(searchTerm);
                flights.Search(searchTerm);
            }

            else if (command == "sort" && splittedInput.Length >= 2)
            {
                var inputData = splittedInput[1];
                var order = "";

                if (splittedInput.Length == 3)
                {
                    order = splittedInput[2];
                }

                if (inputData == "airports")
                {
                    if (order == "descending")
                    {
                        airports.SortDesc();
                    }
                    else
                    {
                        airports.Sort();
                    }

                    Print(airports);
                }
                else if (inputData == "airlines")
                {
                    if (order == "descending")
                    {
                        airlines.SortDesc();
                    }
                    else
                    {
                        airlines.Sort();
                    }

                    Print(airlines);
                }
                else if (inputData == "flights")
                {
                    if (order == "descending")
                    {
                        flights.SortDesc();
                    }
                    else
                    {
                        flights.Sort();
                    }

                    Print(flights);
                }
            }

            else
            {
                System.Console.WriteLine(" Inavlid command!");
            }
        }
    }
}