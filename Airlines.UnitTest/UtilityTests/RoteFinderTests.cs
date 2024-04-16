using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;
using Moq;


namespace Airlines.UnitTests.UtilitiesTests;

public class RouteFinderTests
{
    [Fact]
    public void FindRoute_ReturnsCorrectRouteWithCheapStrategy()
    {
        var logger = new ConsoleLogger();
        var airportManager = new AirportManager(logger);
        var routeFinder = new RouteFinder(airportManager, logger);
        var airport1 = new Airport { Id = "Airport1", Name = "Airport One", City = "City", Country = "Country" };
        var airport2 = new Airport { Id = "Airport2", Name = "Airport Two", City = "City", Country = "Country" };
        var flight1 = new Flight { Id = "Flight1", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Duration = 2, Price = 100 };
        var flight2 = new Flight { Id = "Flight2", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Duration = 3, Price = 150 };
        var flight3 = new Flight { Id = "Flight3", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Duration = 4, Price = 200 };
        airportManager.Add(airport1);
        airportManager.Add(airport2);

        var routeManager = new RouteManager(airportManager, logger);
        routeManager.AddFlight(flight1);
        routeManager.AddFlight(flight2);
        routeManager.AddFlight(flight3);

        var (route, totalDuration, totalPrice, numStops) = routeFinder.FindRoute(airport1, airport2, "cheap", routeManager.Route);

        _ = Assert.Single(route);
        Assert.Equal(100, totalPrice);
        Assert.Equal(2, totalDuration);
        Assert.Equal(1, numStops);
    }

    [Fact]
    public void FindRoute_ReturnsCorrectRouteWithShortStrategy()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeFinder = new RouteFinder(airportManager, loggerMock.Object);
        var airport1 = new Airport { Id = "Airport1", Name = "Airport One", City = "City", Country = "Country" };
        var airport2 = new Airport { Id = "Airport2", Name = "Airport Two", City = "City", Country = "Country" };
        var flight1 = new Flight { Id = "Flight1", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Duration = 2, Price = 100 };
        var flight2 = new Flight { Id = "Flight2", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Duration = 3, Price = 150 };
        var flight3 = new Flight { Id = "Flight3", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Duration = 1, Price = 200 };
        airportManager.Add(airport1);
        airportManager.Add(airport2);

        var routeManager = new RouteManager(airportManager, loggerMock.Object);
        routeManager.AddFlight(flight1);
        routeManager.AddFlight(flight2);
        routeManager.AddFlight(flight3);

        var (route, totalDuration, totalPrice, numStops) = routeFinder.FindRoute(airport1, airport2, "short", routeManager.Route);

        _ = Assert.Single(route);
        Assert.Equal(200, totalPrice);
        Assert.Equal(1, totalDuration);
        Assert.Equal(1, numStops);
    }

    [Fact]
    public void FindRoute_ReturnsCorrectRouteWithStopsStrategy()
    {
        var loggerMock = new Mock<ILogger>();
        var airportManager = new AirportManager(loggerMock.Object);
        var routeFinder = new RouteFinder(airportManager, loggerMock.Object);
        var airport1 = new Airport { Id = "Airport1", Name = "Airport One", City = "City", Country = "Country" };
        var airport2 = new Airport { Id = "Airport2", Name = "Airport Two", City = "City", Country = "Country" };
        var flight1 = new Flight { Id = "Flight1", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Duration = 2, Price = 100 };
        var flight2 = new Flight { Id = "Flight2", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Duration = 3, Price = 150 };
        var flight3 = new Flight { Id = "Flight3", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", Duration = 4, Price = 200 };
        airportManager.Add(airport1);
        airportManager.Add(airport2);

        var routeManager = new RouteManager(airportManager, loggerMock.Object);
        routeManager.AddFlight(flight1);
        routeManager.AddFlight(flight2);
        routeManager.AddFlight(flight3);

        var (route, totalDuration, totalPrice, numStops) = routeFinder.FindRoute(airport1, airport2, "stops", routeManager.Route);

        _ = Assert.Single(route);
        Assert.Equal(100, totalPrice);
        Assert.Equal(2, totalDuration);
        Assert.Equal(1, numStops);
    }
}
