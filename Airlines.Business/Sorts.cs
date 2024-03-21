
namespace Airlines.Business;
public static class Sorts
{
    public static void Sort(this AirportManager manager)
    {
        var airportNames = manager.Airports.Values.Select(airline => airline.Name).ToList().OrderBy(name => name).ToList(); ;

    }

    public static void Sort(this AirlineManager manager)
    {
        var airlineNames = manager.Airlines.Values.Select(airline => airline.Name).ToList().OrderBy(name => name).ToList();
    }

    public static void Sort(this FlightManager manager)
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
    }

    public static void SortDesc(this AirportManager manager)
    {
        var airportNames = manager.Airports.Values.Select(airline => airline.Name).ToList();
        airportNames = airportNames.OrderByDescending(name => name).ToList();
    }

    public static void SortDesc(this AirlineManager manager)
    {
        var airlineNames = manager.Airlines.Values.Select(airline => airline.Name).ToList();
        airlineNames = airlineNames.OrderBy(name => name).ToList();

        airlineNames.Sort();
    }

    public static void SortDesc(this FlightManager manager)
    {
        Sort(manager);
        manager.Flights.Reverse();

    }
}
