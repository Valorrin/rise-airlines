using Airlines.Business.Commands;
using Airlines.Business.Managers;
using Airlines.Business.Utilities;
using Airlines.Business.Validation;
using Moq;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class BatchModeTests
{
    [Fact]
    public void ProcessCommand_BatchModeTrue_AddsCommandToBatchManager()
    {
        var loggerMock = new Mock<ILogger>();
        var invoker = new CommandInvoker();
        var batchManager = new BatchManager();
        var airlineManager = new AirlineManager(loggerMock.Object);
        var airportManager = new AirportManager(loggerMock.Object);
        var flightManager = new FlightManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);
        var aircraftManager = new AircraftManager();
        var reservationManager = new ReservationManager();
        var commandValidator = new CommandValidator(airportManager, flightManager, aircraftManager, routeManager);
        var commandClient = new CommandClient(invoker, airportManager, airlineManager, flightManager, routeManager, reservationManager, batchManager, commandValidator);

        commandClient.ProcessCommand("search searchTerm", isBatchMode: true);

        Assert.Contains(batchManager.Commands, c => c.GetType().Name == "SearchCommand");
    }
}