using Airlines.Business.Commands.ListingCommands;
using Airlines.Business.Managers;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class ListCommandTests
{
    [Fact]
    public void CreateListDataCommand_ReturnsInstanceOfListDataCommand()
    {
        var airportManager = new AirportManager();
        var inputData = "Test Country";
        var from = "Country";

        var command = ListDataCommand.CreateListDataCommand(airportManager, inputData, from);

        Assert.NotNull(command);
        _ = Assert.IsType<ListDataCommand>(command);
    }
}