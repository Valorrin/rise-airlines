using Airlines.Business.Commands.SortCommands;
using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class SortCommandsTests
{
    [Fact]
    public void Execute_DescendingOrder_PrintsDescendingNames()
    {
        var airlineManager = new AirlineManager();
        airlineManager.Airlines.Add(new Airline { Id = "BBB", Name = "Airline2" });
        airlineManager.Airlines.Add(new Airline { Id = "AAA", Name = "Airline1" });
        airlineManager.Airlines.Add(new Airline { Id = "CCC", Name = "Airline3" });

        var command = new SortAirlinesCommand(airlineManager, "descending");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        command.Execute();

        var result = writer.ToString().Trim();
        Assert.Equal("Airline3, Airline2, Airline1", result);
    }

    [Fact]
    public void Execute_AscendingOrder_PrintsAscendingNames()
    {
        var airlineManager = new AirlineManager();
        airlineManager.Airlines.Add(new Airline { Id = "BBB", Name = "Airline2" });
        airlineManager.Airlines.Add(new Airline { Id = "AAA", Name = "Airline1" });
        airlineManager.Airlines.Add(new Airline { Id = "CCC", Name = "Airline3" });

        var command = new SortAirlinesCommand(airlineManager, "ascending");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        command.Execute();

        var result = writer.ToString().Trim();
        Assert.Equal("Airline1, Airline2, Airline3", result);
    }

    [Fact]
    public void CreateSortAirlinesCommand_ReturnsInstance()
    {
        var airlineManager = new AirlineManager();
        var sortOrder = "descending";

        var command = SortAirlinesCommand.CreateSortAirlinesCommand(airlineManager, sortOrder);

        Assert.NotNull(command);
        _ = Assert.IsType<SortAirlinesCommand>(command);
    }

    [Fact]
    public void Execute_DescendingOrder_PrintsAirportsDescendingNames()
    {
        var airportManager = new AirportManager();
        airportManager.Airports.Add(new Airport { Id = "BBB", Name = "Airport2", City = "City2", Country = "Country2" });
        airportManager.Airports.Add(new Airport { Id = "AAA", Name = "Airport1", City = "City1", Country = "Country1" });
        airportManager.Airports.Add(new Airport { Id = "CCC", Name = "Airport3", City = "City3", Country = "Country3" });

        var command = new SortAirportsCommand(airportManager, "descending");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        command.Execute();

        var result = writer.ToString().Trim();
        Assert.Equal("Airport3, Airport2, Airport1", result);
    }

    [Fact]
    public void CreateSortAirportsCommand_ReturnsInstance()
    {
        var airportManager = new AirportManager();
        var sortOrder = "descending";

        var command = SortAirportsCommand.CreateSortAirportsCommand(airportManager, sortOrder);

        Assert.NotNull(command);
        _ = Assert.IsType<SortAirportsCommand>(command);
    }

    [Fact]
    public void Execute_DescendingOrder_PrintsDescendingIds()
    {
        var flightManager = new FlightManager();
        flightManager.Flights.Add(new Flight { Id = "Flight2", DepartureAirport = "BBB", ArrivalAirport = "BBB" });
        flightManager.Flights.Add(new Flight { Id = "Flight1", DepartureAirport = "AAA", ArrivalAirport = "AAA" });
        flightManager.Flights.Add(new Flight { Id = "Flight3", DepartureAirport = "CCC", ArrivalAirport = "CCC" });

        var command = new SortFlightsCommand(flightManager, "descending");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        command.Execute();

        var result = writer.ToString().Trim();
        Assert.Equal("Flight3, Flight2, Flight1", result);
    }

    [Fact]
    public void Execute_AscendingOrder_PrintsAscendingIds()
    {
        var flightManager = new FlightManager();
        flightManager.Flights.Add(new Flight { Id = "Flight2", DepartureAirport = "BBB", ArrivalAirport = "BBB" });
        flightManager.Flights.Add(new Flight { Id = "Flight1", DepartureAirport = "AAA", ArrivalAirport = "AAA" });
        flightManager.Flights.Add(new Flight { Id = "Flight3", DepartureAirport = "CCC", ArrivalAirport = "CCC" });

        var command = new SortFlightsCommand(flightManager, "ascending");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        command.Execute();

        var result = writer.ToString().Trim();
        Assert.Equal("Flight1, Flight2, Flight3", result);
    }

    [Fact]
    public void CreateSortFlightsCommand_ReturnsInstance()
    {
        var flightManager = new FlightManager();
        var sortOrder = "descending";

        var command = SortFlightsCommand.CreateSortFlightsCommand(flightManager, sortOrder);

        Assert.NotNull(command);
        _ = Assert.IsType<SortFlightsCommand>(command);
    }
}
