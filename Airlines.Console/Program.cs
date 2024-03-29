using static Airlines.Console.Utilities.InputReader;
using static Airlines.Console.Utilities.Printer;
using static Airlines.Console.Utilities.FilePathHelper;
using Airlines.Business.Managers;
using Airlines.Business.Commands;

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
        var reservationManager = new ReservationsManager();
        var batchManager = new BatchManager();

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

        var commandInvoker = new CommandInvoker();
        var commandClient = new CommandClient(commandInvoker, airportManager, airlineManager, flightManager, routeManager, aircraftManager, reservationManager, batchManager);

        while (true)
        {
            var command = ReadCommandInput();
            var batchMode = batchManager.BatchMode;
            commandClient.ProcessCommand(command, batchMode);
        }
    }
}