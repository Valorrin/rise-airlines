namespace Airlines.Console.Utilities;
public class InputReader
{
    public static string ReadFromConsole()
    {
        System.Console.WriteLine($"Enter flight name:\n");

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

    public static List<string> ReadFromFile(string filePath)
    {
        var data = new List<string>();

        using (var reader = new StreamReader(filePath))
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line != null)
                    data.Add(line);
            }

        return data;
    }

    public static string ReadCommandInput()
    {
        System.Console.WriteLine($"\nEnter command:\n");

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