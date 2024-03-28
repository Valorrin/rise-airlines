using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;

namespace Airlines.Console.Tests;

public class SortExtensionsTests
{
    [Fact]
    public void SortByName_AirportManager_SortsNamesAlphabetically()
    {
        var airportManager = new AirportManager();
        airportManager.Airports.Add("1", new Airport { Name = "Zulu" });
        airportManager.Airports.Add("2", new Airport { Name = "Alpha" });
        airportManager.Airports.Add("3", new Airport { Name = "Charlie" });

        var sortedNames = airportManager.SortByName();

        Assert.Collection(sortedNames,
            name => Assert.Equal("Alpha", name),
            name => Assert.Equal("Charlie", name),
            name => Assert.Equal("Zulu", name));
    }

    [Fact]
    public void SortByName_AirlineManager_SortsNamesAlphabetically()
    {
        var airlineManager = new AirlineManager();
        airlineManager.Airlines.Add("1", new Airline { Name = "Zulu" });
        airlineManager.Airlines.Add("2", new Airline { Name = "Alpha" });
        airlineManager.Airlines.Add("3", new Airline { Name = "Charlie" });

        var sortedNames = airlineManager.SortByName();

        Assert.Collection(sortedNames,
            name => Assert.Equal("Alpha", name),
            name => Assert.Equal("Charlie", name),
            name => Assert.Equal("Zulu", name));
    }

    [Fact]
    public void SortById_FlightManager_SortsIdsAlphabetically()
    {
        var flightManager = new FlightManager();
        flightManager.Flights.Add(new Flight { Id = "Flight3" });
        flightManager.Flights.Add(new Flight { Id = "Flight1" });
        flightManager.Flights.Add(new Flight { Id = "Flight2" });

        var sortedIds = flightManager.SortById();

        Assert.Collection(sortedIds,
            id => Assert.Equal("Flight1", id),
            id => Assert.Equal("Flight2", id),
            id => Assert.Equal("Flight3", id));
    }

    [Fact]
    public void SortDescByName_AirportManager_SortsNamesDescending()
    {
        var airportManager = new AirportManager();
        airportManager.Airports.Add("1", new Airport { Name = "Zulu" });
        airportManager.Airports.Add("2", new Airport { Name = "Alpha" });
        airportManager.Airports.Add("3", new Airport { Name = "Charlie" });

        var sortedNames = airportManager.SortDescByName();

        Assert.Collection(sortedNames,
            name => Assert.Equal("Zulu", name),
            name => Assert.Equal("Charlie", name),
            name => Assert.Equal("Alpha", name));
    }

    [Fact]
    public void SortDescByName_AirlineManager_SortsNamesDescending()
    {
        var airlineManager = new AirlineManager();
        airlineManager.Airlines.Add("1", new Airline { Name = "Zulu" });
        airlineManager.Airlines.Add("2", new Airline { Name = "Alpha" });
        airlineManager.Airlines.Add("3", new Airline { Name = "Charlie" });

        var sortedNames = airlineManager.SortDescByName();

        Assert.Collection(sortedNames,
            name => Assert.Equal("Zulu", name),
            name => Assert.Equal("Charlie", name),
            name => Assert.Equal("Alpha", name));
    }

    [Fact]
    public void SortDescById_FlightManager_SortsIdsDescending()
    {
        var flightManager = new FlightManager();
        flightManager.Flights.Add(new Flight { Id = "Flight3" });
        flightManager.Flights.Add(new Flight { Id = "Flight1" });
        flightManager.Flights.Add(new Flight { Id = "Flight2" });

        var sortedIds = flightManager.SortDescById();

        Assert.Collection(sortedIds,
            id => Assert.Equal("Flight3", id),
            id => Assert.Equal("Flight2", id),
            id => Assert.Equal("Flight1", id));
    }
}