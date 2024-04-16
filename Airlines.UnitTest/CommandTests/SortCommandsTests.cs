using Airlines.Business.Commands.SortCommands;
using Airlines.Business.Managers;
using Airlines.Business.Utilities;
using Moq;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class SortCommandsTests
{
    [Fact]
    public void CreateSortAirlinesCommand_ReturnsInstance()
    {
        var loggerMock = new Mock<ILogger>();
        var airlineManager = new AirlineManager(loggerMock.Object);
        var sortOrder = "descending";

        var command = new SortAirlinesCommand(airlineManager, sortOrder);

        Assert.NotNull(command);
        _ = Assert.IsType<SortAirlinesCommand>(command);
    }

    [Fact]
    public void CreateSortAirportsCommand_ReturnsInstance()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var sortOrder = "descending";

        var command = new SortAirportsCommand(airportManager, sortOrder);

        Assert.NotNull(command);
        _ = Assert.IsType<SortAirportsCommand>(command);
    }

    [Fact]
    public void CreateSortFlightsCommand_ReturnsInstance()
    {
        var loggerMock = new Mock<ILogger>();
        var flightManager = new FlightManager(loggerMock.Object);
        var sortOrder = "descending";

        var command = new SortFlightsCommand(flightManager, sortOrder);

        Assert.NotNull(command);
        _ = Assert.IsType<SortFlightsCommand>(command);
    }
}