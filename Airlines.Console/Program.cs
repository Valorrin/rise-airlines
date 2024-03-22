using Airlines.Business;
using static Airlines.Console.InputReader;
using static Airlines.Console.Printer;
using static Airlines.Console.FilePathHelper;
using static Airlines.Business.CommandProcess;

namespace Airlines;
public class Program
{
    public static void Main()
    {
        var airportManager = new AirportManager();
        var airlineManager = new AirlineManager();
        var flightManager = new FlightManager();

        var airportFilePath = GetFilePath("airports.csv");
        var airlineFilePath = GetFilePath("airlines.csv");

        var airportData = ReadFromFile(airportFilePath);
        var airlineData = ReadFromFile(airlineFilePath);

        airportManager.Add(airportData);
        airlineManager.Add(airlineData);

        ReadFromConsole(flightManager);

        PrintAll(airportManager, airlineManager, flightManager);

        var command = ReadCommands();
        ExecuteCommand(command, airportManager, airlineManager, flightManager);
    }
}