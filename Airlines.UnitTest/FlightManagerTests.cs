using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Console.Tests;

public class FlightManagerTests
{
    [Fact]
    public void Add_AddsFlightToList()
    {
        var flightManager = new FlightManager();
        var flight = new Flight();

        flightManager.Add(flight);

        Assert.Contains(flight, flightManager.Flights);
    }

    [Fact]
    public void Search_WhenSearchTermIsNull_PrintsErrorMessage()
    {
        var flightManager = new FlightManager();
        var writer = new StringWriter();
        System.Console.SetOut(writer);

        flightManager.Search(null!);

        var result = writer.ToString();
        Assert.Contains(" Error: search term cannot be null or empty!", result);
    }

    [Fact]
    public void Search_WhenSearchTermIsEmpty_PrintsErrorMessage()
    {
        var flightManager = new FlightManager();
        var writer = new StringWriter();
        System.Console.SetOut(writer);

        flightManager.Search("");

        var result = writer.ToString();
        Assert.Contains(" Error: search term cannot be null or empty!", result);
    }

    [Fact]
    public void Search_WhenSearchTermDoesNotExist_PrintsErrorMessage()
    {
        var flightManager = new FlightManager();
        var writer = new StringWriter();
        System.Console.SetOut(writer);
        flightManager.Flights.Add(new Flight { Id = "Flight1" });

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
        flightManager.Flights.Add(new Flight { Id = "Flight1" });

        flightManager.Search("Flight1");

        var result = writer.ToString();
        Assert.Contains("Flight1 is Flight name.", result);
    }
}