using Airlines.Business;
using static Airlines.Console.InputReader;
using static Airlines.Console.Printer;

namespace Airlines;

public class Program
{
    public static void Main()
    {
        var airports = new AirportManager();
        var airlines = new AirlineManager();
        var flights = new FlightManager();

        ReadInput(airports);
        ReadInput(airlines);
        ReadInput(flights);

        Print(airports);
        Print(airlines);
        Print(flights);

        _ = ReadCommands(airports, airlines, flights);
    }
}