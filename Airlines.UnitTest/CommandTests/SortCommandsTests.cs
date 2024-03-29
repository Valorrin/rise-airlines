using Airlines.Business.Commands.SortCommands;
using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.CommandTests;
public class SortCommandsTests
{
    [Fact]
    public void Execute_DescendingOrder_PrintsDescendingNames()
    {
        var airlineManager = new AirlineManager();
        airlineManager.Airlines.Add("2", new Airline { Name = "Airline2" });
        airlineManager.Airlines.Add("1", new Airline { Name = "Airline1" });
        airlineManager.Airlines.Add("3", new Airline { Name = "Airline3" });

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
        airlineManager.Airlines.Add("2", new Airline { Name = "Airline2" });
        airlineManager.Airlines.Add("1", new Airline { Name = "Airline1" });
        airlineManager.Airlines.Add("3", new Airline { Name = "Airline3" });

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
        airportManager.Airports.Add("2", new Airport { Name = "Airport2" });
        airportManager.Airports.Add("1", new Airport { Name = "Airport1" });
        airportManager.Airports.Add("3", new Airport { Name = "Airport3" });

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
        flightManager.Flights.Add(new Flight { Id = "Flight2" });
        flightManager.Flights.Add(new Flight { Id = "Flight1" });
        flightManager.Flights.Add(new Flight { Id = "Flight3" });

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
        flightManager.Flights.Add(new Flight { Id = "Flight2" });
        flightManager.Flights.Add(new Flight { Id = "Flight1" });
        flightManager.Flights.Add(new Flight { Id = "Flight3" });

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
