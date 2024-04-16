using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;
using Moq;

namespace Airlines.UnitTests.ManagerTests;

[Collection("Sequential")]
public class AirportManagerTests
{
    private readonly ConsoleLogger _logger = new();

    [Fact]
    public void Add_Airport_Successfully()
    {
        var airportManager = new AirportManager(_logger);
        var airport = new Airport
        {
            Id = "ABC",
            Name = "Test Airport",
            City = "Test City",
            Country = "Test Country"
        };

        airportManager.Add(airport);

        Assert.Contains(airport, airportManager.Airports);
    }

    [Fact]
    public void Search_AirportDoesNotExist_DoesNotLog()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);

        airportManager.Search("Nonexistent Airport");

        loggerMock.Verify(logger => logger.Log(It.IsAny<string>()), Times.Never);
    }


    [Theory]
    [InlineData("ExistingAirport")]
    public void Exist_AirportExists_LoggedExistence(string airportName)
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var existingAirport = new Airport { Id = "1", Name = airportName, City = "City One", Country = "Country One" };
        airportManager.Add(existingAirport);

        airportManager.Exist(airportName);

        loggerMock.Verify(logger => logger.Log($"{airportName} exists."), Times.Once);
    }

    [Theory]
    [InlineData("NonExistingAirport")]
    public void Exist_AirportDoesNotExist_LoggedNonExistence(string airportName)
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var existingAirport = new Airport { Id = "1", Name = "ExistingAirport", City = "City One", Country = "Country One" };
        airportManager.Add(existingAirport);

        airportManager.Exist(airportName);

        loggerMock.Verify(logger => logger.Log($"{airportName} does not exist."), Times.Once);
    }
    [Fact]
    public void GetAirportById_ReturnsAirport_WhenAirportExists()
    {
        var airportManager = new AirportManager(_logger);
        var expectedAirport = new Airport { Id = "1", Name = "Airport One", City = "City One", Country = "Country One" };
        airportManager.Airports.Add(expectedAirport);

        var airport = airportManager.GetAirportById("1");

        Assert.Equal(expectedAirport, airport);
    }

    [Fact]
    public void GetAirportById_ReturnsNull_WhenAirportDoesNotExist()
    {
        var airportManager = new AirportManager(_logger);

        var airport = airportManager.GetAirportById("NonExistingId");

        Assert.Null(airport);
    }
}