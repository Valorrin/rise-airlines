namespace Airlines.Console.Utilities;
public class InputReader
{
    public static IEnumerable<string> ReadFromFile(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    yield return line;
                }
            }
        }
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