using Airlines.Business;

namespace Airlines.Console;
public class Printer
{
    public static void Print(AirlineManager manager)
    {
        System.Console.Write($"Airlines:\n");
        foreach (var airline in manager.Airlines.Values)
        {
            System.Console.WriteLine($" Airline name: {airline.Name}");
        }
    }
    public static void Print(AirportManager manager)
    {
        System.Console.Write($"Airports:\n");
        foreach (var airport in manager.Airports.Values)
        {
            System.Console.WriteLine($" Airport name: {airport.Name}");
        }
    }
    public static void Print(FlightManager manager)
    {
        System.Console.Write($" Flights: ");
        System.Console.WriteLine(string.Join(", ", manager.Flights));

    }
}
