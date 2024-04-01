using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.ManagerTests;

public class FlightManagerTests
{
    [Fact]
    public void Add_AddsFlightToList()
    {
        var flightManager = new FlightManager();
        var flight = new Flight { Id = "F1", DepartureAirport = "AAA", ArrivalAirport = "BBB" };

        flightManager.Add(flight);

        Assert.Contains(flight, flightManager.Flights);
    }

    [Fact]
    public void Search_WhenSearchTermDoesNotExist_PrintsErrorMessage()
    {
        var flightManager = new FlightManager();
        var writer = new StringWriter();
        System.Console.SetOut(writer);
        flightManager.Flights.Add(new Flight { Id = "Flight1", DepartureAirport = "AAA", ArrivalAirport = "BBB" });

        flightManager.Search("Flight2");

        var result = writer.ToString();
        Assert.DoesNotContain("Flight2 is Flight name.", result);
    }

    [Fact]
    public void Search_WhenSearchTermExists_PrintsMessage()
    {
        var flightManager = new FlightManager();
        var writer = new StringWriter();
        System.Console.SetOut(writer);
        flightManager.Flights.Add(new Flight { Id = "Flight1", DepartureAirport = "AAA", ArrivalAirport = "BBB" });

        flightManager.Search("Flight1");

        var result = writer.ToString();
        Assert.Contains("Flight1 is Flight name.", result);
    }
}