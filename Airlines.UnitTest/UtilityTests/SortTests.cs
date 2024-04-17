using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;
using Moq;

namespace Airlines.UnitTests.UtilityTests;

[Collection("Sequential")]
public class SortTests
{
    [Fact]
    public void SortByName_AirportManager_SortsNamesAlphabetically()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        airportManager.Add(new Airport { Id = "1", Name = "Zulu", City = "City1", Country = "Country1" });
        airportManager.Add(new Airport { Id = "2", Name = "Alpha", City = "City2", Country = "Country2" });
        airportManager.Add(new Airport { Id = "3", Name = "Charlie", City = "City3", Country = "Country3" });


        airportManager.SortByName();

        Assert.Collection(airportManager.Airports,
            name => Assert.Equal("Alpha", airportManager.Airports[0].Name),
            name => Assert.Equal("Charlie", airportManager.Airports[1].Name),
            name => Assert.Equal("Zulu", airportManager.Airports[2].Name));
    }

    [Fact]
    public void SortByName_AirlineManager_SortsNamesAlphabetically()
    {
        var loggerMock = new Mock<ILogger>();
        var airlineManager = new AirlineManager(loggerMock.Object);
        airlineManager.Add(new Airline { Id = "1", Name = "Zulu" });
        airlineManager.Add(new Airline { Id = "2", Name = "Alpha" });
        airlineManager.Add(new Airline { Id = "3", Name = "Charlie" });

        airlineManager.SortByName();

        Assert.Collection(airlineManager.Airlines,
             name => Assert.Equal("Alpha", airlineManager.Airlines[0].Name),
             name => Assert.Equal("Charlie", airlineManager.Airlines[1].Name),
             name => Assert.Equal("Zulu", airlineManager.Airlines[2].Name));
    }

    [Fact]
    public void SortById_FlightManager_SortsIdsAlphabetically()
    {
        var loggerMock = new Mock<ILogger>();
        var flightManager = new FlightManager(loggerMock.Object);
        flightManager.Add(new Flight { Id = "Flight3", DepartureAirport = "AAA", ArrivalAirport = "BBB", Duration = 10, Price = 5.5 });
        flightManager.Add(new Flight { Id = "Flight1", DepartureAirport = "CCC", ArrivalAirport = "DDD", Duration = 10, Price = 5.5 });
        flightManager.Add(new Flight { Id = "Flight2", DepartureAirport = "EEE", ArrivalAirport = "FFF", Duration = 10, Price = 5.5 });

        flightManager.SortById();

        Assert.Collection(flightManager.Flights,
            id => Assert.Equal("Flight1", flightManager.Flights[0].Id),
            id => Assert.Equal("Flight2", flightManager.Flights[1].Id),
            id => Assert.Equal("Flight3", flightManager.Flights[2].Id));
    }

    [Fact]
    public void SortDescByName_AirportManager_SortsNamesDescending()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        airportManager.Airports.Add(new Airport { Id = "1", Name = "Zulu", City = "City1", Country = "Country1" });
        airportManager.Airports.Add(new Airport { Id = "2", Name = "Alpha", City = "City2", Country = "Country2" });
        airportManager.Airports.Add(new Airport { Id = "3", Name = "Charlie", City = "City3", Country = "Country3" });

        airportManager.SortDescByName();

        Assert.Collection(airportManager.Airports,
            name => Assert.Equal("Zulu", airportManager.Airports[0].Name),
            name => Assert.Equal("Charlie", airportManager.Airports[1].Name),
            name => Assert.Equal("Alpha", airportManager.Airports[2].Name));
    }

    [Fact]
    public void SortDescByName_AirlineManager_SortsNamesDescending()
    {
        var loggerMock = new Mock<ILogger>();
        var airlineManager = new AirlineManager(loggerMock.Object);
        airlineManager.Airlines.Add(new Airline { Id = "1", Name = "Zulu" });
        airlineManager.Airlines.Add(new Airline { Id = "2", Name = "Alpha" });
        airlineManager.Airlines.Add(new Airline { Id = "3", Name = "Charlie" });

        airlineManager.SortDescByName();

        Assert.Collection(airlineManager.Airlines,
            name => Assert.Equal("Zulu", airlineManager.Airlines[0].Name),
            name => Assert.Equal("Charlie", airlineManager.Airlines[1].Name),
            name => Assert.Equal("Alpha", airlineManager.Airlines[2].Name));
    }

    [Fact]
    public void SortDescById_FlightManager_SortsIdsDescending()
    {
        var loggerMock = new Mock<ILogger>();
        var flightManager = new FlightManager(loggerMock.Object);
        flightManager.Add(new Flight { Id = "FL333", DepartureAirport = "AAA", ArrivalAirport = "BBB", Duration = 10, Price = 5.5 });
        flightManager.Add(new Flight { Id = "FL111", DepartureAirport = "CCC", ArrivalAirport = "DDD", Duration = 10, Price = 5.5 });
        flightManager.Add(new Flight { Id = "FL222", DepartureAirport = "EEE", ArrivalAirport = "FFF", Duration = 10, Price = 5.5 });

        flightManager.SortDescById();

        Assert.Collection(flightManager.Flights,
            id => Assert.Equal("FL333", flightManager.Flights[0].Id),
            id => Assert.Equal("FL222", flightManager.Flights[1].Id),
            id => Assert.Equal("FL111", flightManager.Flights[2].Id));
    }
}