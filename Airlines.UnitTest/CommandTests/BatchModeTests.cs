using Airlines.Business.Commands;
using Airlines.Business.Managers;

namespace Airlines.UnitTests.CommandTests;
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
        var routeManager = new RouteManager();
        var aircraftManager = new AircraftManager();
        var reservationManager = new ReservationsManager();
        var commandClient = new CommandClient(invoker, airportManager, airlineManager, flightManager, routeManager, aircraftManager, reservationManager, batchManager);

        commandClient.ProcessCommand("search searchTerm", batchMode: true);

        Assert.Contains(batchManager.Commands, c => c.GetType().Name == "SearchCommand");
    }
}