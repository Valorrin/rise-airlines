
namespace Airlines.Console;
public static class Sort
{
    public static string[] SortAirports(this string[] airports, bool ascending = true)
    {
        var n = airports.Length;
        string temp;

        for (var j = 0; j < n - 1; j++)
        {
            for (var i = j + 1; i < n; i++)
            {
                if (string.Compare(airports[j], airports[i]) > 0)
                {
                    temp = airports[j];
                    airports[j] = airports[i];
                    airports[i] = temp;
                }
            }
        }

        if (!ascending)
        {
            _ = airports.Reverse();
        }

        return airports;
    }

    public static string[] SortAirlines(this string[] airlines, bool ascending = true)
    {
        var n = airlines.Length;

        for (var i = 0; i < n - 1; i++)
        {
            var minIndex = i;

            for (var j = i + 1; j < n; j++)
            {
                if (string.Compare(airlines[j], airlines[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                (airlines[minIndex], airlines[i]) = (airlines[i], airlines[minIndex]);
            }
        }

        if (!ascending)
        {
            _ = airlines.Reverse();
        }

        return airlines;
    }

    public static string[] SortFlights(this string[] flights, bool ascending = true)
    {
        var n = flights.Length;

        for (var i = 0; i < n - 1; i++)
        {
            var minIndex = i;

            for (var j = i + 1; j < n; j++)
            {
                if (string.Compare(flights[j], flights[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                (flights[minIndex], flights[i]) = (flights[i], flights[minIndex]);
            }
        }

        if (!ascending)
        {
            _ = flights.Reverse();
        }

        return flights;
    }
}
