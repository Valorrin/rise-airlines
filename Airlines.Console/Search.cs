namespace Airlines.Console;
public static class Search
{
    public static int LinearSearch(List<string> data, string target)
    {
        for (var i = 0; i < data.Count; i++)
        {
            if (data[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    public static int BinarySearch(List<string> data, string target)
    {
        int l = 0, r = data.Count - 1;
        while (l <= r)
        {
            var m = l + ((r - l) / 2);

            var comparisonResult = string.Compare(data[m], target);

            if (comparisonResult == 0)
                return m;

            if (comparisonResult < 0)
                l = m + 1;
            else
                r = m - 1;
        }

        return -1;
    }

    public static string PrintSearchResult(List<string> airports, List<string> airlines, List<string> flights, string searchTerm)
    {
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
