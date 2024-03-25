using static Airlines.Console.InputReader;
using static Airlines.Console.Printer;
using static Airlines.Console.FilePathHelper;
using static Airlines.Business.Utilities.CommandProcess;
using Airlines.Business.Managers;

namespace Airlines;
public class Program
{
    public static void Main()
    {
        var airportManager = new AirportManager();
        var airlineManager = new AirlineManager();
        var flightManager = new FlightManager();
        var routeManager = new RouteManager();
        var aircraftManager = new AircraftManager();

        var airportFilePath = GetFilePath("airports.csv");
        var airlineFilePath = GetFilePath("airlines.csv");
        var flightFilePath = GetFilePath("flights.csv");
        var aircraftFilePath = GetFilePath("aircrafts.csv");

        var airportData = ReadFromFile(airportFilePath);
        var airlineData = ReadFromFile(airlineFilePath);
        var flightData = ReadFromFile(flightFilePath);
        var aircraftData = ReadFromFile(aircraftFilePath);

        airportManager.Add(airportData);
        airlineManager.Add(airlineData);
        flightManager.Add(flightData);
        aircraftManager.Add(aircraftData);

        PrintAll(airportManager, airlineManager, flightManager, aircraftManager);

        while (true)
        {
            var command = ReadCommands();
            ExecuteCommand(command, airportManager, airlineManager, flightManager, routeManager);
        }
    }
}