using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests;
public class RouteManagerTests
{
    [Fact]
    public void AddFlight_ShouldAddFlightToRoutes()
    {
        var routeManager = new RouteManager();
        var flight = new Flight { Id = "F1", DepartureAirport = "D1", ArrivalAirport = "A1" };

        routeManager.AddFlight(flight);

        Assert.Contains(flight, routeManager.Routes);
    }

    [Fact]
    public void RemoveFlight_ShouldRemoveLastFlightFromRoutes()
    {
        var routeManager = new RouteManager();
        var flight1 = new Flight { Id = "F1", DepartureAirport = "D1", ArrivalAirport = "A1" };
        var flight2 = new Flight { Id = "F2", DepartureAirport = "D2", ArrivalAirport = "A2" };
        routeManager.AddFlight(flight1);
        routeManager.AddFlight(flight2);

        routeManager.RemoveFlight();

        Assert.DoesNotContain(flight2, routeManager.Routes);
    }

    [Fact]
    public void Validate_WhenRoutesIsEmpty_ShouldReturnTrue()
    {
        var routeManager = new RouteManager();
        var flight = new Flight { Id = "F1", DepartureAirport = "D1", ArrivalAirport = "A1" };

        var result = routeManager.Validate(flight);

        Assert.True(result);
    }

    [Fact]
    public void Validate_WhenDepartureAirportMatchesArrivalAirportOfLastFlight_ShouldReturnTrue()
    {
        var routeManager = new RouteManager();
        var flight1 = new Flight { Id = "F1", DepartureAirport = "D1", ArrivalAirport = "A1" };
        var flight2 = new Flight { Id = "F2", DepartureAirport = "A1", ArrivalAirport = "A2" };
        routeManager.AddFlight(flight1);

        var result = routeManager.Validate(flight2);

        Assert.True(result);
    }

    [Fact]
    public void Validate_WhenDepartureAirportDoesNotMatchArrivalAirportOfLastFlight_ShouldReturnFalse()
    {
        var routeManager = new RouteManager();
        var flight1 = new Flight { Id = "F1", DepartureAirport = "D1", ArrivalAirport = "A1" };
        var flight2 = new Flight { Id = "F2", DepartureAirport = "D2", ArrivalAirport = "A2" };
        routeManager.AddFlight(flight1);

        var result = routeManager.Validate(flight2);

        Assert.False(result);
    }

    [Fact]
    public void IsEmpty_WhenRoutesIsEmpty_ShouldReturnTrue()
    {
        var routeManager = new RouteManager();

        var result = routeManager.IsEmpty();

        Assert.True(result);
    }

    [Fact]
    public void IsEmpty_WhenRoutesIsNotEmpty_ShouldReturnFalse()
    {
        var routeManager = new RouteManager();
        routeManager.AddFlight(new Flight { Id = "F1", DepartureAirport = "D1", ArrivalAirport = "A1" });

        var result = routeManager.IsEmpty();

        Assert.False(result);
    }
}