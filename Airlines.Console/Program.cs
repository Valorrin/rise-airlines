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
        var routeManager = new RouteManager(airportManager);
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
        var routeFilePath = GetFilePath("route.csv");

        var airportData = ReadFromFile(airportFilePath);
        var airlineData = ReadFromFile(airlineFilePath);
        var flightData = ReadFromFile(flightFilePath);
        var aircraftData = ReadFromFile(aircraftFilePath);
        var routeData = ReadFromFile(routeFilePath);

        try
        {
            inputValidator.ValidateAirportData(airportData);
            inputValidator.ValidateAirlineData(airlineData);
            inputValidator.ValidateFlightData(flightData);
            inputValidator.ValidateAircraftData(aircraftData);
            inputValidator.ValidateRouteData(routeData);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($" Error: {ex.Message}");
            System.Console.WriteLine("Please ensure that the provided data files contain valid information.");
            return;
        }

        airportManager.Add(airportData);
        airlineManager.Add(airlineData);
        flightManager.Add(flightData);
        aircraftManager.Add(aircraftData);
        routeManager.Add(routeData, airportManager, flightManager);

        printer.PrintAll();

        while (true)
        {
            var commandInput = ReadCommandInput();
            var batchMode = batchManager.BatchMode;

            try
            {
                inputValidator.ValidateCommandInputData(commandInput);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($" Error: {ex.Message}");
                continue;
            }

            commandClient.ProcessCommand(commandInput, batchMode);
        }
    }
}