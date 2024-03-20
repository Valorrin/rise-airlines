using Airlines.Business;
using System.IO;
using static Airlines.Business.CommandProcess;

namespace Airlines.Console;
public class InputReader
{
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

            if (manager.Validate(input))
            {
                manager.Add(input);
            }
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

            if (manager.Validate(input))
            {
                manager.Add(input);
            }
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

            if (manager.Validate(input))
            {
                manager.Add(input);
            }
        }
    }
    public static string ReadCommands(AirportManager airports, AirlineManager airlines, FlightManager flights)
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

            ProcessCommand(input, airports, airlines, flights);
        }
    }

    public static List<string> ReadDataFromFile(string filePath)
    {
        var data = new List<string>();

        using (var reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line != null)
                {
                    data.Add(line);
                }

                System.Console.WriteLine($"Line readed!");
            }
        }

        return data;
    }
}
