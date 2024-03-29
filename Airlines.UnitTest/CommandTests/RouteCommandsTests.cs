using Airlines.Business.Commands.RouteCommands;
using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.CommandTests;
public class RouteCommandsTests
{
    [Fact]
    public void Execute_AddsFlightToRouteManager()
    {
        var routeManager = new RouteManager();
        var flight = new Flight();
        var command = RouteAddCommand.CreateRouteAddCommand(routeManager, flight);

        command.Execute();

        Assert.Contains(flight, routeManager.Routes);
    }

    [Fact]
    public void CreateRouteAddCommand_ReturnsInstanceOfRouteAddCommand()
    {
        var routeManager = new RouteManager();
        var flight = new Flight();

        var command = RouteAddCommand.CreateRouteAddCommand(routeManager, flight);

        Assert.NotNull(command);
        _ = Assert.IsType<RouteAddCommand>(command);
    }

    [Fact]
    public void Execute_ClearsRoutes()
    {
        var routeManager = new RouteManager();
        _ = routeManager.Routes.AddLast(new Flight());

        var command = RouteNewCommand.CreateRouteNewCommand(routeManager);

        command.Execute();

        Assert.Empty(routeManager.Routes);
    }

    [Fact]
    public void CreateRouteNewCommand_ReturnsInstance()
    {
        var routeManager = new RouteManager();

        var command = RouteNewCommand.CreateRouteNewCommand(routeManager);

        Assert.NotNull(command);
        _ = Assert.IsType<RouteNewCommand>(command);
    }

    [Fact]
    public void Execute_PrintsRoutes()
    {
        var routeManager = new RouteManager();
        routeManager.AddFlight(new Flight { Id = "F1" });
        routeManager.AddFlight(new Flight { Id = "F2" });

        var command = RoutePrintCommand.CreateRoutePrintCommand(routeManager);

        var consoleOutput = new StringWriter();
        System.Console.SetOut(consoleOutput);

        command.Execute();
        var output = consoleOutput.ToString().Trim();

        Assert.Contains("Flight ID: F1", output);
        Assert.Contains("Flight ID: F2", output);
    }

    [Fact]
    public void CreateRoutePrintCommand_ReturnsInstance()
    {
        var routeManager = new RouteManager();

        var command = RoutePrintCommand.CreateRoutePrintCommand(routeManager);

        Assert.NotNull(command);
        _ = Assert.IsType<RoutePrintCommand>(command);
    }

    [Fact]
    public void Execute_RemovesFlight()
    {
        var routeManager = new RouteManager();
        routeManager.AddFlight(new Flight { Id = "F1" });
        routeManager.AddFlight(new Flight { Id = "F2" });

        var command = RouteRemoveCommand.CreateRouteRemoveCommand(routeManager);

        command.Execute();

        _ = Assert.Single(routeManager.Routes);
    }

    [Fact]
    public void CreateRouteRemoveCommand_ReturnsInstance()
    {
        var routeManager = new RouteManager();

        var command = RouteRemoveCommand.CreateRouteRemoveCommand(routeManager);

        Assert.NotNull(command);
        _ = Assert.IsType<RouteRemoveCommand>(command);
    }
}