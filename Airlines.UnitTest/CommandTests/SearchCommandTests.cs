using Airlines.Business.Commands.SearchCommands;
using Airlines.Business.Managers;
using Airlines.Business.Utilities;
using Moq;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class SearchCommandTests
{

    [Fact]
    public void CreateSearchCommand_ReturnsInstance()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var airlineManager = new AirlineManager(loggerMock.Object);
        var flightManager = new FlightManager(loggerMock.Object);
        var searchTerm = "test";

        var command = new SearchCommand(airportManager, airlineManager, flightManager, searchTerm);

        Assert.NotNull(command);
        _ = Assert.IsType<SearchCommand>(command);
    }
}