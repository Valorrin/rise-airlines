using Airlines.Business;

namespace Airlines.Console;
public class Printer
{
    public static void Print(AirlineManager manager)
    {
        System.Console.Write($" Airlines: ");
        System.Console.WriteLine(string.Join(", ", manager.Airlines.Values));
    }
    public static void Print(AirportManager manager)
    {
        System.Console.Write($" Airports: ");
        System.Console.WriteLine(string.Join(", ", manager.Airports.Values));

    }
    public static void Print(FlightManager manager)
    {
        System.Console.Write($" Flights: ");
        System.Console.WriteLine(string.Join(", ", manager.Flights));

    }
}
