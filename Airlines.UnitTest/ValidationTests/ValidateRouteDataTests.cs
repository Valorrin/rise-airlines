using Airlines.Business;
using Airlines.Business.Exceptions;
using Airlines.Business.Managers;

namespace Airlines.UnitTests.ConsoleTests;

[Collection("Sequential")]
public class ValidateRouteDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;
    private readonly RouteManager _routeManager;
    private readonly InputValidator _inputValidator;

    public ValidateRouteDataTests()
    {
        _airportManager = new AirportManager();
        _airlineManager = new AirlineManager();
        _flightManager = new FlightManager();
        _aircraftManager = new AircraftManager();
        _routeManager = new RouteManager(_airportManager);

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
}