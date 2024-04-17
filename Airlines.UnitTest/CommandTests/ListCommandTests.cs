using Airlines.Business.Commands.ListingCommands;
using Airlines.Business.Managers;
using Airlines.Business.Utilities;
using Moq;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class ListCommandTests
{
    [Fact]
    public void CreateListDataCommand_ReturnsInstanceOfListDataCommand()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var inputData = "Test Country";
        var from = "Country";

        var command = new ListDataCommand(airportManager, inputData, from);

        Assert.NotNull(command);
        _ = Assert.IsType<ListDataCommand>(command);
    }
}