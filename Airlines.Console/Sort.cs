
namespace Airlines.Console;
public static class Sort
{
    public static void SortAirports(this AirportManager manager, bool ascending = true)
    {
        var n = manager.Airports.Count;
        string temp;

        for (var j = 0; j < n - 1; j++)
        {
            for (var i = j + 1; i < n; i++)
            {
                if (string.Compare(manager.Airports[j], manager.Airports[i]) > 0)
                {
                    temp = manager.Airports[j];
                    manager.Airports[j] = manager.Airports[i];
                    manager.Airports[i] = temp;
                }
            }
        }

        if (!ascending)
        {
            manager.Airports.Reverse();
        }
    }

    public static void SortAirlines(this List<string> airlines, bool ascending = true)
    {
        var n = airlines.Count;

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
            airlines.Reverse();
        }
    }

    public static void SortFlights(this List<string> flights, bool ascending = true)
    {
        var n = flights.Count;

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
            flights.Reverse();
        }
    }
}
