using Airlines.Business.Commands.SearchCommands;
using Airlines.Business.Managers;
using Airlines.Business.Utilities;
using Moq;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class ExistCommandTests
{
    [Fact]
    public void CreateCheckAirportExistenceCommand_ReturnsInstance()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var airlineName = "TestAirline";

        var command = new CheckAirportExistenceCommand(airportManager, airlineName);

        Assert.NotNull(command);
        _ = Assert.IsType<CheckAirportExistenceCommand>(command);
    }
}