using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.ManagerTests;
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
}