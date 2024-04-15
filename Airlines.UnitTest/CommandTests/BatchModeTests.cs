using Airlines.Business.Commands;
using Airlines.Business.Managers;
using Airlines.Business.Utilities;
using Airlines.Business.Validation;
using Castle.Core.Logging;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class BatchModeTests
{
    [Fact]
    public void ProcessCommand_BatchModeTrue_AddsCommandToBatchManager()
    {
        var logger = new Business.Utilities.ConsoleLogger();
        var invoker = new CommandInvoker();
        var batchManager = new BatchManager();
        var airlineManager = new AirlineManager(logger);
        var airportManager = new AirportManager(logger);
        var flightManager = new FlightManager(logger);
        var routeManager = new RouteManager(airportManager, logger);
        var aircraftManager = new AircraftManager();
        var reservationManager = new ReservationManager();
        var commandValidator = new CommandValidator(airportManager, flightManager, aircraftManager, routeManager);
        var commandClient = new CommandClient(invoker, airportManager, airlineManager, flightManager, routeManager, reservationManager, batchManager, commandValidator);

        commandClient.ProcessCommand("search searchTerm", batchMode: true);

        Assert.Contains(batchManager.Commands, c => c.GetType().Name == "SearchCommand");
    }
}