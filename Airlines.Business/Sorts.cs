namespace Airlines.Business;
public static class Sorts
{
    public static List<string> SortByName(this AirportManager manager)
    {
        var airportNames = manager.Airports.Values.Select(airline => airline.Name).ToList().OrderBy(name => name).ToList();

        return airportNames;
    }

    public static List<string> SortByName(this AirlineManager manager)
    {
        var airlineNames = manager.Airlines.Values.Select(airline => airline.Name).ToList().OrderBy(name => name).ToList();

        return airlineNames;
    }

    public static List<string> SortById(this FlightManager manager)
    {
        var flightIds = manager.Flights.Select(flight => flight.Id).ToList().OrderBy(name => name).ToList();

        return flightIds;
    }

    public static List<string> SortDescByName(this AirportManager manager)
    {
        var airportNames = manager.Airports.Values.Select(airline => airline.Name).ToList().OrderByDescending(name => name).ToList();

        return airportNames;
    }

    public static List<string> SortDescByName(this AirlineManager manager)
    {
        var airlineNames = manager.Airlines.Values.Select(airline => airline.Name).ToList().OrderByDescending(name => name).ToList();

        return airlineNames;
    }

    public static List<string> SortDescById(this FlightManager manager)
    {
        var flightIds = manager.Flights.Select(flight => flight.Id).ToList().OrderByDescending(name => name).ToList();

        return flightIds;
    }
}