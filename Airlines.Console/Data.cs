namespace Airlines.Console;
public static class Data
{
    public static List<string> AddData(string item, List<string> data)
    {
        data.Add(item);
        System.Console.WriteLine($" {item} was added successfully!");

        return data;
    }

    public static void PrintData(string type, List<string> data)
    {
        System.Console.Write($" {type}s: ");

        foreach (var item in data)
        {
            System.Console.Write($" {item} ");
        }
        System.Console.WriteLine();
    }
}
