using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;
using Moq;

namespace Airlines.UnitTests.ManagerTests;

[Collection("Sequential")]
public class RouteManagerTests
{
    [Fact]
    public void AddFlight_AddsFlightToRoute()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);

        var airport1 = new Airport { Id = "Air1", Name = "Airport One", City = "City One", Country = "Country One" };
        var airport2 = new Airport { Id = "Air2", Name = "Airport Two", City = "City Two", Country = "Country Two" };
        var airport3 = new Airport { Id = "Air3", Name = "Airport Three", City = "City Three", Country = "Country Three" };
        var flight = new Flight { Id = "Fl1", DepartureAirport = "Air1", ArrivalAirport = "Air2", Duration = 10, Price = 5.5 };

        airportManager.Add(airport1);
        airportManager.Add(airport2);
        airportManager.Add(airport3);
        routeManager.AddFlight(flight);

        Assert.Contains(flight, routeManager.Route.AdjacencyList.Values.SelectMany(list => list));
    }

    [Fact]
    public void IsConnected_StartAirportIsNotConnectedToEndAirport_ReturnsFalse()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);

        var airport1 = new Airport { Id = "Air1", Name = "Airport One", City = "City One", Country = "Country One" };
        var airport2 = new Airport { Id = "Air2", Name = "Airport Two", City = "City Two", Country = "Country Two" };
        var airport3 = new Airport { Id = "Air3", Name = "Airport Three", City = "City Three", Country = "Country Three" };

        airportManager.Add(airport1);
        airportManager.Add(airport2);
        airportManager.Add(airport3);
        routeManager.AddFlight(new Flight { Id = "Fl1", DepartureAirport = "Air1", ArrivalAirport = "Air2", Duration = 10, Price = 5.5 });

        var isConnected = routeManager.IsConnected(airport1, airport3);

        Assert.False(isConnected);
    }


    [Fact]
    public void IsConnected_StartAirportIsConnectedToEndAirportDirectly_ReturnsTrue()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);

        var airport1 = new Airport { Id = "Air1", Name = "Airport One", City = "City One", Country = "Country One" };
        var airport2 = new Airport { Id = "Air2", Name = "Airport Two", City = "City Two", Country = "Country Two" };
        var airport3 = new Airport { Id = "Air3", Name = "Airport Three", City = "City Three", Country = "Country Three" };

        airportManager.Add(airport1);
        airportManager.Add(airport2);
        airportManager.Add(airport3);
        routeManager.AddFlight(new Flight { Id = "Fl1", DepartureAirport = "Air1", ArrivalAirport = "Air2", Duration = 10, Price = 5.5 });

        var isConnected = routeManager.IsConnected(airport1, airport2);

        Assert.True(isConnected);
    }

    [Fact]
    public void IsConnected_StartAirportIsConnectedToEndAirportThroughIntermediateStops_ReturnsTrue()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);

        var airport1 = new Airport { Id = "Air1", Name = "Airport One", City = "City One", Country = "Country One" };
        var airport2 = new Airport { Id = "Air2", Name = "Airport Two", City = "City Two", Country = "Country Two" };
        var airport3 = new Airport { Id = "Air3", Name = "Airport Three", City = "City Three", Country = "Country Three" };

        airportManager.Add(airport1);
        airportManager.Add(airport2);
        airportManager.Add(airport3);
        routeManager.AddFlight(new Flight { Id = "Fl1", DepartureAirport = "Air1", ArrivalAirport = "Air2", Duration = 10, Price = 5.5 });
        routeManager.AddFlight(new Flight { Id = "Fl2", DepartureAirport = "Air2", ArrivalAirport = "Air3", Duration = 10, Price = 5.5 });

        var isConnected = routeManager.IsConnected(airport1, airport3);

        Assert.True(isConnected);
    }

    [Fact]
    public void AddAirport_AddsAirportToRoute()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);
        var airport = new Airport { Id = "Air1", Name = "Airport One", City = "City One", Country = "Country One" };

        routeManager.AddAirport(airport);

        Assert.Contains(airport, routeManager.Route.AdjacencyList.Keys);
    }

    [Fact]
    public void Find_ReturnsFalse_WhenDestinationAirportDoesNotExist()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);

        var result = routeManager.Find("NonExistingAirportId");

        Assert.False(result);
    }

    [Fact]
    public void Find_ReturnsFalse_WhenNoRouteExistsToDestinationAirport()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);
        var airport1 = new Airport { Id = "1", Name = "Airport One", City = "City One", Country = "Country One" };
        airportManager.Add(airport1);

        var result = routeManager.Find("1");

        Assert.False(result);
    }

    [Fact]
    public void Find_ReturnsTrue_WhenRouteExistsToDestinationAirport()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);
        var airport1 = new Airport { Id = "1", Name = "Airport One", City = "City One", Country = "Country One" };
        var airport2 = new Airport { Id = "2", Name = "Airport Two", City = "City Two", Country = "Country Two" };
        airportManager.Add(airport1);
        airportManager.Add(airport2);
        routeManager.AddFlight(new Flight { Id = "F1", DepartureAirport = "1", ArrivalAirport = "2", Duration = 10, Price = 5.5 });

        var result = routeManager.Find("2");

        Assert.True(result);
    }

    [Fact]
    public void RemoveLastFlight_RemovesLastFlightFromRoute()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeManager = new RouteManager(airportManager, loggerMock.Object);
        var airport1 = new Airport { Id = "Airport1", Name = "Airport One", City = "City", Country = "Country" };
        var airport2 = new Airport { Id = "Airport2", Name = "Airport Two", City = "City", Country = "Country" };
        var flight1 = new Flight { Id = "F1", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Price = 10, Duration = 10 };
        var flight2 = new Flight { Id = "F2", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Price = 10, Duration = 10 };

        airportManager.Add(airport1);
        airportManager.Add(airport2);
        routeManager.AddFlight(flight1);
        routeManager.AddFlight(flight2);

        routeManager.RemoveLastFlight();

        Assert.DoesNotContain(flight2, routeManager.Route.AdjacencyList.Values.SelectMany(list => list));
    }
}