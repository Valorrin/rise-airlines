using static Airlines.Console.Utilities.InputReader;
using static Airlines.Console.Utilities.FilePathHelper;
using Airlines.Business.Managers;
using Airlines.Business.Commands;
using Airlines.Console.Utilities;
using Airlines.Business;
using Airlines.Business.Mapping;
using Airlines.Business.Validation;
using Airlines.Business.Utilities;
using Airlines.Persistence.Services;


namespace Airlines;
public class Program
{
    public static void Main()
    {
        var logger = new ConsoleLogger();

        var airportManager = new AirportManager(logger);
        var airlineManager = new AirlineManager(logger);
        var flightManager = new FlightManager(logger);
        var routeManager = new RouteManager(airportManager, logger);
        var aircraftManager = new AircraftManager();
        var reservationManager = new ReservationManager();
        var batchManager = new BatchManager();

        var airportService = new AirportService();
        var airlineService = new AirlineService();
        var flightService = new FlightService();

        var inputValidator = new InputValidator(airportManager, airlineManager, flightManager);
        var printer = new Printer(airportManager, airlineManager, flightManager, aircraftManager);
        var mapper = new ObjectMapper();

        var commandInvoker = new CommandInvoker();
        var commandValidator = new CommandValidator(airportManager, flightManager, aircraftManager, routeManager);
        var commandClient = new CommandClient(commandInvoker, airportManager, airlineManager, flightManager, routeManager, reservationManager, batchManager, commandValidator);

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
            foreach (var line in airportData)
            {
                inputValidator.ValidateAirportData(line);
                var airport = mapper.MapToAirport(line);
                airportManager.Add(airport);
            }

            foreach (var line in airlineData)
            {
                inputValidator.ValidateAirlineData(line);
                var airline = mapper.MapToAirline(line);
                airlineManager.Add(airline);
            }

            foreach (var line in flightData)
            {
                inputValidator.ValidateFlightData(line);
                var flight = mapper.MapToFlight(line);
                flightManager.Add(flight);
            }

            foreach (var line in aircraftData)
            {
                inputValidator.ValidateAircraftData(line);
                aircraftManager.Add(line);
            }

            foreach (var line in routeData)
            {
                inputValidator.ValidateRouteData(line);
                var flight = flightManager.GetFlightById(line);
                routeManager.AddFlight(flight);
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($" Error: {ex.Message}");
            System.Console.WriteLine("Please ensure that the provided data files contain valid information.");
            return;
        }

        //printer.PrintAll();

        while (true)
        {
            var commandInput = ReadCommandInput();
            var isBatchMode = batchManager.IsBatchMode;

            try
            {
                commandValidator.ValidateCommandArguments(commandInput);
                commandClient.ProcessCommand(commandInput, isBatchMode);

                var logs = logger.GetLogs();
                printer.Print(logs);
                logger.ClearLogs();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($" Error: {ex.Message}");
                continue;
            }
        }
    }
}