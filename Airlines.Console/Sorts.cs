
namespace Airlines.Console;
public static class Sorts
{
    public static void Sort(this AirportManager manager, bool ascending = true)
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

    public static void Sort(this AirlineManager manager, bool ascending = true)
    {
        var n = manager.Airlines.Count;

        for (var i = 0; i < n - 1; i++)
        {
            var minIndex = i;

            for (var j = i + 1; j < n; j++)
            {
                if (string.Compare(manager.Airlines[j], manager.Airlines[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                (manager.Airlines[minIndex], manager.Airlines[i]) = (manager.Airlines[i], manager.Airlines[minIndex]);
            }
        }

        if (!ascending)
        {
            manager.Airlines.Reverse();
        }
    }

    public static void Sort(this FlightManager manager, bool ascending = true)
    {
        var n = manager.Flights.Count;

        for (var i = 0; i < n - 1; i++)
        {
            var minIndex = i;

            for (var j = i + 1; j < n; j++)
            {
                if (string.Compare(manager.Flights[j], manager.Flights[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                (manager.Flights[minIndex], manager.Flights[i]) = (manager.Flights[i], manager.Flights[minIndex]);
            }
        }

        if (!ascending)
        {
            manager.Flights.Reverse();
        }
    }
}
