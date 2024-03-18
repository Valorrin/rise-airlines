namespace Airlines.Console;
public static class Data
{
    public static string[] AddData(string item, string[] data)
    {

        for (var i = 0; i < data.Length; i++)
        {
            if (data[i] == null)
            {
                data[i] = item;

                break;
            }
        }

        System.Console.WriteLine($" {item} was added successfully!");

        return data;
    }

    public static void PrintData(string type, string[] data)
    {
        System.Console.Write($" {type}s: ");

        for (var i = 0; i < data.Length; i++)
        {
            if (data[i] != null)
            {
                System.Console.Write($" {data[i]} ");
            }
        }
        System.Console.WriteLine();
    }
}
