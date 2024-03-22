using Airlines.Business;
using static Airlines.Business.CommandProcess;

namespace Airlines.Console;
public class InputReader
{
    public static void ReadFromConsole(FlightManager manager)
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

    public static List<string> ReadFromFile(string filePath)
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
            }
        }

        return data;
    }

    public static string ReadCommands()
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

            return input;
        }
    }
}
