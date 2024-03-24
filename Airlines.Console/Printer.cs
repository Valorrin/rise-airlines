using Airlines.Business;

namespace Airlines.Console;
public class Printer
{
    public static void Print(AirportManager manager)
    {
        System.Console.Write($"Airports:\n");
        foreach (var airport in manager.Airports.Values)
        {
            System.Console.WriteLine($" Airport name: {airport.Name}");
            System.Console.WriteLine($" Airport city: {airport.City}");
            System.Console.WriteLine($" Airport country: {airport.Country}\n");
        }
    }
    public static void Print(AirlineManager manager)
    {
        System.Console.Write($"Airlines:\n");
        foreach (var airline in manager.Airlines.Values)
        {
            System.Console.WriteLine($" Airline name: {airline.Name}\n");
        }
    }
    public static void Print(FlightManager manager)
    {
        System.Console.Write($"Flights: \n");
        foreach (var flight in manager.Flights)
        {
            System.Console.WriteLine($" Flight id: {flight.Id}");
        }
    }
    public static void PrintAll(AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager)
    {
        Print(airportManager);
        Print(airlineManager);
        Print(flightManager);
    }
}
