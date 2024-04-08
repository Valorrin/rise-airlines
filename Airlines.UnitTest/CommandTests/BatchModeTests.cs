using Airlines.Business.Commands;
using Airlines.Business.Managers;
using Airlines.Business.Validation;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class BatchModeTests
{
    [Fact]
    public void ProcessCommand_BatchModeTrue_AddsCommandToBatchManager()
    {
        var invoker = new CommandInvoker();
        var batchManager = new BatchManager();
        var airlineManager = new AirlineManager();
        var airportManager = new AirportManager();
        var flightManager = new FlightManager();
        var routeManager = new RouteManager(airportManager);
        var aircraftManager = new AircraftManager();
        var reservationManager = new ReservationManager();
        var commandValidator = new CommandValidator(airportManager, flightManager, aircraftManager, routeManager);
        var commandClient = new CommandClient(invoker, airportManager, airlineManager, flightManager, routeManager, reservationManager, batchManager, commandValidator);

        commandClient.ProcessCommand("search searchTerm", batchMode: true);

        Assert.Contains(batchManager.Commands, c => c.GetType().Name == "SearchCommand");
    }
}