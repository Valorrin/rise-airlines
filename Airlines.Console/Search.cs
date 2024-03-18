namespace Airlines.Console;
public static class Search
{
    public static int LinearSearch(string[] array, string target)
    {
        for (var i = 0; i < array.Length; i++)
        {
            if (array[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    public static int BinarySearch(string[] arr, string target)
    {
        int l = 0, r = arr.Length - 1;
        while (l <= r)
        {
            var m = l + ((r - l) / 2);

            var comparisonResult = string.Compare(arr[m], target);

            if (comparisonResult == 0)
                return m;

            if (comparisonResult < 0)
                l = m + 1;
            else
                r = m - 1;
        }

        return -1;
    }

    public static void PrintSearchResult(string[] airports, string[] airlines, string[] flights)
    {
        System.Console.WriteLine($"\nEnter search term or type 'done' to finish:\n");

        while (true)
        {
            var searchTerm = System.Console.ReadLine();

            var termFound = false;

            if (string.IsNullOrEmpty(searchTerm))
            {
                System.Console.WriteLine(" Error: search term cannot be null or empty!");
                continue;
            }

            if (searchTerm == "done")
            {
                System.Console.WriteLine();
                break;
            }

            if (BinarySearch(airports, searchTerm) >= 0)
            {
                termFound = true;
                System.Console.WriteLine($" {searchTerm} is Airport name.");
            }

            if (BinarySearch(airlines, searchTerm) >= 0)
            {
                termFound = true;
                System.Console.WriteLine($" {searchTerm} is Airline name.");
            }

            if (BinarySearch(flights, searchTerm) >= 0)
            {
                termFound = true;
                System.Console.WriteLine($" {searchTerm} is Flight name.");
            }

            if (!termFound)
            {
                System.Console.WriteLine($" {searchTerm} was not found.");
            }
        }
    }
}
