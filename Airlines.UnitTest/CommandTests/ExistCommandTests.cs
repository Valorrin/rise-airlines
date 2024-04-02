using Airlines.Business.Commands.SearchCommands;
using Airlines.Business.Managers;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class ExistCommandTests
{
    [Fact]
    public void CreateCheckAirportExistenceCommand_ReturnsInstance()
    {
        var airportManager = new AirportManager();
        var airlineName = "TestAirline";

        var command = CheckAirportExistenceCommand.CreateCheckAirportExistenceCommand(airportManager, airlineName);

        Assert.NotNull(command);
        _ = Assert.IsType<CheckAirportExistenceCommand>(command);
    }
}