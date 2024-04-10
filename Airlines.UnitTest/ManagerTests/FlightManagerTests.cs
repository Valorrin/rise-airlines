using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.ManagerTests;

[Collection("Sequential")]
public class FlightManagerTests
{
    [Fact]
    public void Add_AddsFlightToList()
    {
        var flightManager = new FlightManager();
        var flight = new Flight { Id = "F1", DepartureAirport = "AAA", ArrivalAirport = "BBB", Duration = 10, Price = 5.5 };

        flightManager.Add(flight);

        Assert.Contains(flight, flightManager.Flights);
    }

    [Fact]
    public void Search_WhenSearchTermDoesNotExist_PrintsErrorMessage()
    {
        var flightManager = new FlightManager();
        var writer = new StringWriter();
        Console.SetOut(writer);
        flightManager.Flights.Add(new Flight { Id = "Flight1", DepartureAirport = "AAA", ArrivalAirport = "BBB", Duration = 10, Price = 5.5 });

        flightManager.Search("Flight2");

        var result = writer.ToString();
        Assert.DoesNotContain("Flight2 is Flight name.", result);
    }

    [Fact]
    public void Search_WhenSearchTermExists_PrintsMessage()
    {
        var flightManager = new FlightManager();
        var writer = new StringWriter();
        Console.SetOut(writer);
        flightManager.Flights.Add(new Flight { Id = "Flight1", DepartureAirport = "AAA", ArrivalAirport = "BBB", Duration = 10, Price = 5.5 });

        flightManager.Search("Flight1");

        var result = writer.ToString();
        Assert.Contains("Flight1 is Flight name.", result);
    }

    [Fact]
    public void GetAircraftModel_ReturnsAircraftModel()
    {
        var flightManager = new FlightManager();
        var flight = new Flight { Id = "F1", DepartureAirport = "AAA", ArrivalAirport = "BBB", AircraftModel = "Boeing 747", Duration = 10, Price = 5.5 };
        flightManager.Flights.Add(flight);

        var aircraftModel = flightManager.GetAircraftModel("F1");

        Assert.Equal("Boeing 747", aircraftModel);
    }

    [Fact]
    public void GetAircraftModel_WhenFlightIdDoesNotExist_ReturnsNull()
    {
        var flightManager = new FlightManager();

        var aircraftModel = flightManager.GetAircraftModel("NonExistingFlightId");

        Assert.Null(aircraftModel);
    }

    [Fact]
    public void GetFlightById_ReturnsFlight()
    {
        var flightManager = new FlightManager();
        var expectedFlight = new Flight { Id = "F1", DepartureAirport = "AAA", ArrivalAirport = "BBB", Duration = 10, Price = 5.5 };
        flightManager.Flights.Add(expectedFlight);

        var flight = flightManager.GetFlightById("F1");

        Assert.Equal(expectedFlight, flight);
    }

    [Fact]
    public void GetFlightById_WhenFlightIdDoesNotExist_ReturnsNull()
    {
        var flightManager = new FlightManager();

        var flight = flightManager.GetFlightById("NonExistingFlightId");

        Assert.Null(flight);
    }
}