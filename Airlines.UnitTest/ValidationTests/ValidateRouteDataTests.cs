using Airlines.Business;
using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;
using Airlines.Business.Validation;

namespace Airlines.UnitTests.ConsoleTests;

[Collection("Sequential")]
public class ValidateRouteDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly RouteManager _routeManager;
    private readonly AircraftManager _aircraftManager;
    private readonly InputValidator _inputValidator;
    private readonly CommandValidator _commandValidator;
    private readonly ConsoleLogger _consoleLogger;


    public ValidateRouteDataTests()
    {
        _consoleLogger = new ConsoleLogger();
        _airportManager = new AirportManager(_consoleLogger);
        _airlineManager = new AirlineManager(_consoleLogger);
        _flightManager = new FlightManager(_consoleLogger);
        _aircraftManager = new AircraftManager();
        _routeManager = new RouteManager(_airportManager, _consoleLogger);
        _commandValidator = new CommandValidator(
            _airportManager,
            _flightManager,
            _aircraftManager,
            _routeManager);
        _inputValidator = new InputValidator(
            _airportManager,
            _airlineManager,
            _flightManager
        );
    }

    [Fact]
    public void ParseRouteData_EmptyData_ThrowsEmptyRouteException()
    {
        var emptyData = "";

        var exception = Assert.Throws<FlightNotFoundException>(() => _inputValidator.ValidateRouteData(emptyData));
    }

    [Fact]
    public void ValidateRouteCommand_AddAction_NullFlightToAdd_ThrowsInvalidCommandArgumentException()
    {
        var routeManager = new RouteManager(new AirportManager(_consoleLogger), _consoleLogger);
        Flight nullFlight = null!;
        var startAirport = new Airport { Id = "StartAirport", Name = "Start Airport", City = "City", Country = "Country" };
        var endAirport = new Airport { Id = "EndAirport", Name = "End Airport", City = "City", Country = "Country" };

        _ = Assert.Throws<InvalidCommandArgumentException>(() => _commandValidator.ValidateRouteCommand("add", nullFlight, startAirport, endAirport, "strategy"));
    }

    [Fact]
    public void ValidateRouteCommand_RemoveAction_EmptyRoute_ThrowsEmptyRouteException()
    {
        var routeManager = new RouteManager(new AirportManager(_consoleLogger), _consoleLogger);

        _ = Assert.Throws<EmptyRouteException>(() => _commandValidator.ValidateRouteCommand("remove", null!, null!, null!, null!));
    }

    [Theory]
    [InlineData("check")]
    [InlineData("search")]
    public void ValidateRouteCommand_CheckOrSearchAction_NullStartAirport_ThrowsInvalidCommandArgumentException(string commandAction)
    {
        var routeManager = new RouteManager(new AirportManager(_consoleLogger), _consoleLogger);
        Airport startAirport = null!;
        var endAirport = new Airport { Id = "EndAirport", Name = "End Airport", City = "City", Country = "Country" };

        _ = Assert.Throws<InvalidCommandArgumentException>(() => _commandValidator.ValidateRouteCommand(commandAction, null!, startAirport, endAirport, "strategy"));
    }

    [Theory]
    [InlineData("check")]
    [InlineData("search")]
    public void ValidateRouteCommand_CheckOrSearchAction_NullEndAirport_ThrowsInvalidCommandArgumentException(string commandAction)
    {
        var routeManager = new RouteManager(new AirportManager(_consoleLogger), _consoleLogger);
        var startAirport = new Airport { Id = "StartAirport", Name = "Start Airport", City = "City", Country = "Country" };
        Airport? endAirport = null;

        _ = Assert.Throws<InvalidCommandArgumentException>(() => _commandValidator.ValidateRouteCommand(commandAction, null!, startAirport, endAirport!, "strategy"));
    }

    [Theory]
    [InlineData("check")]
    [InlineData("search")]
    public void ValidateRouteCommand_CheckOrSearchAction_SameStartAndEndAirport_ThrowsInvalidCommandArgumentException(string commandAction)
    {
        var routeManager = new RouteManager(new AirportManager(_consoleLogger), _consoleLogger);
        var startAirport = new Airport { Id = "SameAirport", Name = "Same Airport", City = "City", Country = "Country" };
        var endAirport = new Airport { Id = "SameAirport", Name = "Same Airport", City = "City", Country = "Country" };

        _ = Assert.Throws<AirportNotFoundException>(() => _commandValidator.ValidateRouteCommand(commandAction, null!, startAirport, endAirport, "strategy"));
    }

    [Theory]
    [InlineData("check")]
    [InlineData("search")]
    public void ValidateRouteCommand_CheckOrSearchAction_StartAirportNotFound_ThrowsAirportNotFoundException(string commandAction)
    {
        var airportManager = new AirportManager(_consoleLogger);
        var routeManager = new RouteManager(airportManager, _consoleLogger);
        var startAirport = new Airport { Id = "StartAirport", Name = "Start Airport", City = "City", Country = "Country" };
        var endAirport = new Airport { Id = "EndAirport", Name = "End Airport", City = "City", Country = "Country" };
        airportManager.Add(endAirport);

        _ = Assert.Throws<AirportNotFoundException>(() => _commandValidator.ValidateRouteCommand(commandAction, null!, startAirport, endAirport, "strategy"));
    }

    [Theory]
    [InlineData("check")]
    [InlineData("search")]
    public void ValidateRouteCommand_CheckOrSearchAction_EndAirportNotFound_ThrowsAirportNotFoundException(string commandAction)
    {
        var airportManager = new AirportManager(_consoleLogger);
        var routeManager = new RouteManager(airportManager, _consoleLogger);
        var startAirport = new Airport { Id = "StartAirport", Name = "Start Airport", City = "City", Country = "Country" };
        var endAirport = new Airport { Id = "EndAirport", Name = "End Airport", City = "City", Country = "Country" };
        airportManager.Add(startAirport);

        _ = Assert.Throws<AirportNotFoundException>(() => _commandValidator.ValidateRouteCommand(commandAction, null!, startAirport, endAirport, "strategy"));
    }
}