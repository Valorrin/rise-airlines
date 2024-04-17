using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;
using Moq;

namespace Airlines.UnitTests.ManagerTests;

[Collection("Sequential")]
public class AirlineManagerTests
{
    [Fact]
    public void Add_AirlineAddedToList()
    {
        var loggerMock = new Mock<ILogger>();
        var airlineManager = new AirlineManager(loggerMock.Object);
        var airline = new Airline { Id = "ABC", Name = "Test Airline" };

        airlineManager.Add(airline);

        Assert.Contains(airline, airlineManager.Airlines);
    }

    [Fact]
    public void Search_AirlineExists_LoggedExistence()
    {
        var loggerMock = new Mock<ILogger>();
        var airlineManager = new AirlineManager(loggerMock.Object);
        var searchTerm = "Test Airline";
        airlineManager.Add(new Airline { Id = "ABC", Name = searchTerm });

        airlineManager.Search(searchTerm);

        loggerMock.Verify(logger => logger.Log($" {searchTerm} is Airline name."), Times.Once);
    }
}