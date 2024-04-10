using Airlines.Business;
using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
<<<<<<< HEAD
=======
using Airlines.Business.Models;
using Airlines.Business.Validation;
>>>>>>> Task-19-Enhanced-Flight-Route-Search

namespace Airlines.UnitTests.ConsoleTests;

[Collection("Sequential")]
public class ValidateRouteDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
<<<<<<< HEAD
=======
    private readonly RouteManager _routeManager;
    private readonly AircraftManager _aircraftManager;
>>>>>>> Task-19-Enhanced-Flight-Route-Search
    private readonly InputValidator _inputValidator;
    private readonly CommandValidator _commandValidator;

    public ValidateRouteDataTests()
    {
        _airportManager = new AirportManager();
        _airlineManager = new AirlineManager();
        _flightManager = new FlightManager();
<<<<<<< HEAD
=======
        _aircraftManager = new AircraftManager();
        _routeManager = new RouteManager(_airportManager);
        _commandValidator = new CommandValidator(
            _airportManager,
            _flightManager,
            _aircraftManager,
            _routeManager);
>>>>>>> Task-19-Enhanced-Flight-Route-Search
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
<<<<<<< HEAD
=======
    }

    [Fact]
    public void ValidateRouteCommand_AddAction_NullFlightToAdd_ThrowsInvalidCommandArgumentException()
    {
        var routeManager = new RouteManager(new AirportManager());
        Flight nullFlight = null!;
        var startAirport = new Airport { Id = "StartAirport", Name = "Start Airport", City = "City", Country = "Country" };
        var endAirport = new Airport { Id = "EndAirport", Name = "End Airport", City = "City", Country = "Country" };

        _ = Assert.Throws<InvalidCommandArgumentException>(() => _commandValidator.ValidateRouteCommand("add", nullFlight, startAirport, endAirport, "strategy"));
    }

    [Fact]
    public void ValidateRouteCommand_RemoveAction_EmptyRoute_ThrowsEmptyRouteException()
    {
        var routeManager = new RouteManager(new AirportManager());

        _ = Assert.Throws<EmptyRouteException>(() => _commandValidator.ValidateRouteCommand("remove", null!, null!, null!, null!));
    }

    [Theory]
    [InlineData("check")]
    [InlineData("search")]
    public void ValidateRouteCommand_CheckOrSearchAction_NullStartAirport_ThrowsInvalidCommandArgumentException(string commandAction)
    {
        var routeManager = new RouteManager(new AirportManager());
        Airport startAirport = null!;
        var endAirport = new Airport { Id = "EndAirport", Name = "End Airport", City = "City", Country = "Country" };

        _ = Assert.Throws<InvalidCommandArgumentException>(() => _commandValidator.ValidateRouteCommand(commandAction, null!, startAirport, endAirport, "strategy"));
    }

    [Theory]
    [InlineData("check")]
    [InlineData("search")]
    public void ValidateRouteCommand_CheckOrSearchAction_NullEndAirport_ThrowsInvalidCommandArgumentException(string commandAction)
    {
        var routeManager = new RouteManager(new AirportManager());
        var startAirport = new Airport { Id = "StartAirport", Name = "Start Airport", City = "City", Country = "Country" };
        Airport? endAirport = null;

        _ = Assert.Throws<InvalidCommandArgumentException>(() => _commandValidator.ValidateRouteCommand(commandAction, null!, startAirport, endAirport!, "strategy"));
    }

    [Theory]
    [InlineData("check")]
    [InlineData("search")]
    public void ValidateRouteCommand_CheckOrSearchAction_SameStartAndEndAirport_ThrowsInvalidCommandArgumentException(string commandAction)
    {
        var routeManager = new RouteManager(new AirportManager());
        var startAirport = new Airport { Id = "SameAirport", Name = "Same Airport", City = "City", Country = "Country" };
        var endAirport = new Airport { Id = "SameAirport", Name = "Same Airport", City = "City", Country = "Country" };

        _ = Assert.Throws<AirportNotFoundException>(() => _commandValidator.ValidateRouteCommand(commandAction, null!, startAirport, endAirport, "strategy"));
    }

    [Theory]
    [InlineData("check")]
    [InlineData("search")]
    public void ValidateRouteCommand_CheckOrSearchAction_StartAirportNotFound_ThrowsAirportNotFoundException(string commandAction)
    {
        var airportManager = new AirportManager();
        var routeManager = new RouteManager(airportManager);
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
        var airportManager = new AirportManager();
        var routeManager = new RouteManager(airportManager);
        var startAirport = new Airport { Id = "StartAirport", Name = "Start Airport", City = "City", Country = "Country" };
        var endAirport = new Airport { Id = "EndAirport", Name = "End Airport", City = "City", Country = "Country" };
        airportManager.Add(startAirport);

        _ = Assert.Throws<AirportNotFoundException>(() => _commandValidator.ValidateRouteCommand(commandAction, null!, startAirport, endAirport, "strategy"));
>>>>>>> Task-19-Enhanced-Flight-Route-Search
    }
}