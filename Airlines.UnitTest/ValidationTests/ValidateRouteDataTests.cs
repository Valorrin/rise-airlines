using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
using Airlines.Console;
using Airlines.Console.Exceptions;

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
        _routeManager = new RouteManager();

        _inputValidator = new InputValidator(
            _airportManager,
            _airlineManager,
            _flightManager,
            _aircraftManager,
            _routeManager
        );
    }

    [Fact]
    public void ParseRouteData_EmptyData_ThrowsEmptyRouteException()
    {
        var emptyData = new List<string>();

        var exception = Assert.Throws<EmptyRouteException>(() => _inputValidator.ValidateRouteData(emptyData));
    }

    [Theory]
    [InlineData("A")]
    [InlineData("ABCDA")]
    public void ParseRouteData_InvalidStartAirportId_ThrowsInvalidIdLengthException(string startAirportId)
    {
        var data = new List<string> { startAirportId };

        var exception = Assert.Throws<InvalidIdLengthException>(() => _inputValidator.ValidateRouteData(data));
    }

    [Theory]
    [InlineData("$#@")]
    public void ParseRouteData_InvalidStartAirportId_ThrowsInvalidIdCharactersException(string startAirportId)
    {
        var data = new List<string> { startAirportId };

        var exception = Assert.Throws<InvalidIdCharactersException>(() => _inputValidator.ValidateRouteData(data));
    }

    [Theory]
    [InlineData("")]
    public void ParseRouteData_InvalidStartAirportId_ThrowsInvalidInputException(string startAirportId)
    {
        var data = new List<string> { startAirportId };

        var exception = Assert.Throws<InvalidInputException>(() => _inputValidator.ValidateRouteData(data));
    }

    [Fact]
    public void ParseRouteData_InvalidStartAirportIdCharacters_ThrowsInvalidIdCharactersException()
    {
        var invalidId = "AB@";
        var data = new List<string> { invalidId };

        var exception = Assert.Throws<InvalidIdCharactersException>(() => _inputValidator.ValidateRouteData(data));
    }
}