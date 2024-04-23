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
using Airlines.Persistence.Dto;


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

        airportService.PrintAllAirports();
        airportService.PrintAllAirports("City", "Dallas");
        airportService.AddAirport(new AirportDto("ATL", "Hartsfield-Jackson Atlanta International Airport", "USA", "Atlanta", 105, 6, new DateOnly(1980, 09, 1)));
        airportService.UpdateAirport("ATL", new AirportDto("ATL", "Edited edited edited", "EDITED", "EDITED", 999, 9, new DateOnly(2000, 01, 1)));
        airportService.DeleteAirport("ATL");

        airlineService.PrintAllAirlines();
        airlineService.PrintAllAirlines("Name", "AAR");
        airlineService.AddAirline(new AirlineDto("BBB", new DateOnly(2008, 11, 8), 40, "Description"));
        airlineService.UpdateAirline(1, new AirlineDto("AAA", new DateOnly(2008, 07, 30), 40, "Edited"));
        airlineService.DeleteAirline(6);

        flightService.PrintAllFlights();
        flightService.PrintAllFlights("ArrivalAirportId", "ORD");
        flightService.AddFlight(new FlightDto("FL222", 1, "JFK", "LAX", new DateTime(2025, 4, 16, 14, 30, 0), new DateTime(2025, 4, 18, 18, 27, 0), 100));
        flightService.UpdateFlight(5, new FlightDto("FL222", 1, "JFK", "LAX", new DateTime(2025, 4, 16, 14, 30, 0), new DateTime(2025, 4, 18, 18, 27, 0), 100));
        flightService.DeleteFlight(6);

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