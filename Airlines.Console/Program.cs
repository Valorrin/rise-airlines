using static Airlines.Console.Utilities.InputReader;
using static Airlines.Console.Utilities.FilePathHelper;
using Airlines.Business.Managers;
using Airlines.Business.Commands;
using Airlines.Console.Utilities;
using Airlines.Console;

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

        var inputValidator = new InputValidator(airportManager, airlineManager, flightManager, aircraftManager, routeManager);
        var printer = new Printer(airportManager, airlineManager, flightManager, aircraftManager);

        var commandInvoker = new CommandInvoker();
        var commandClient = new CommandClient(commandInvoker, airportManager, airlineManager, flightManager, routeManager, reservationManager, batchManager);

        var airportFilePath = GetFilePath("airports.csv");
        var airlineFilePath = GetFilePath("airlines.csv");
        var flightFilePath = GetFilePath("flights.csv");
        var aircraftFilePath = GetFilePath("aircrafts.csv");

        var airportData = ReadFromFile(airportFilePath);
        var airlineData = ReadFromFile(airlineFilePath);
        var flightData = ReadFromFile(flightFilePath);
        var aircraftData = ReadFromFile(aircraftFilePath);

        inputValidator.ValidateAirportData(airportData);
        inputValidator.ValidateAirlineData(airlineData);
        inputValidator.ValidateFlightData(flightData);
        inputValidator.ValidateAircraftData(aircraftData);

        airportManager.Add(airportData);
        airlineManager.Add(airlineData);
        flightManager.Add(flightData);
        aircraftManager.Add(aircraftData);

        printer.PrintAll();

        while (true)
        {
            var commandInput = ReadCommandInput();
            var batchMode = batchManager.BatchMode;

            if (inputValidator.ValidateCommandInputData(commandInput)) commandClient.ProcessCommand(commandInput, batchMode);
        }
    }
}