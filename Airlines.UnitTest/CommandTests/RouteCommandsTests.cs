using Airlines.Business;
using Airlines.Business.Commands.RouteCommands;
using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class RouteCommandsTests
{
    [Fact]
    public void CreateRouteAddCommand_ReturnsInstanceOfRouteAddCommand()
    {
        var routeManager = new RouteManager();
        var tree = new 
            
            
            
            
            
            ("DFW");
        routeManager.Add(tree);
        var flight = new Flight { Id = "F1", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", AircraftModel = "Model1" };

        var command = RouteAddCommand.CreateRouteAddCommand(routeManager, flight, "DFW");

        Assert.NotNull(command);
        _ = Assert.IsType<RouteAddCommand>(command);
    }

    [Fact]
    public void CreateRouteNewCommand_ReturnsInstance()
    {
        var routeManager = new RouteManager();
        var tree = new FlightRouteTree("DFW");

        var command = RouteNewCommand.CreateRouteNewCommand(routeManager, tree);

        Assert.NotNull(command);
        _ = Assert.IsType<RouteNewCommand>(command);
    }

    [Fact]
    public void CreateRoutePrintCommand_ReturnsInstance()
    {
        var routeManager = new RouteManager();
        var tree = new FlightRouteTree("DFW");
        routeManager.Add(tree);

        var command = RoutePrintCommand.CreateRoutePrintCommand(routeManager, "DFW");

        Assert.NotNull(command);
        _ = Assert.IsType<RoutePrintCommand>(command);
    }

    [Fact]
    public void Execute_RemovesFlight()
    {
        var routeManager = new RouteManager();
        var tree = new FlightRouteTree("DFW");
        routeManager.Add(tree);

        routeManager.AddFlight(new Flight { Id = "F1", DepartureAirport = "Airport1", ArrivalAirport = "Airport2", AircraftModel = "Model1" }, "DFW");
        routeManager.AddFlight(new Flight { Id = "F2", DepartureAirport = "Airport2", ArrivalAirport = "Airport3", AircraftModel = "Model1" }, "DFW");

        var command = RouteRemoveCommand.CreateRouteRemoveCommand(routeManager, "DFW");

        command.Execute();

        _ = Assert.Single(routeManager.Routes);
    }

    [Fact]
    public void CreateRouteRemoveCommand_ReturnsInstance()
    {
        var routeManager = new RouteManager();
        var tree = new FlightRouteTree("DFW");
        routeManager.Add(tree);

        var command = RouteRemoveCommand.CreateRouteRemoveCommand(routeManager, "DFW");

        Assert.NotNull(command);
        _ = Assert.IsType<RouteRemoveCommand>(command);
    }
}