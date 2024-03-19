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

        airports.ReadInput();
        airlines.ReadInput();
        flights.ReadInput();

        airports.Print();
        airlines.Print();
        flights.Print();

        airports.Sort();
        airlines.Sort();
        flights.Sort();

        ReadCommands(airports, airlines, flights);
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

                    airports.Print();
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

                    airlines.Print();
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

                    flights.Print();
                }
            }

            else
            {
                System.Console.WriteLine(" Inavlid command!");
            }
        }
    }
}