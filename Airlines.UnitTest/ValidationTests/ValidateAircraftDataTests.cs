using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Console;
using Airlines.Console.Exceptions;

namespace Airlines.UnitTests.ConsoleTests;

public class ValidateAircraftDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;
    private readonly RouteManager _routeManager;
    private readonly InputValidator _inputValidator;

    public ValidateAircraftDataTests()
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

    [Theory]
    [InlineData("JFK")] // Valid data
    [InlineData("123")] // Valid data
    [InlineData("12AD")] // Valid data
    public void ValidateAircraftData_ValidData_NoExceptionThrown(string data)
    {
        try
        {
            _inputValidator.ValidateAircraftData(data);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception: {ex.Message}");
        }
    }

    [Theory]
    [InlineData("")]
    public void ValidateAircraftData_InvalidNameCharacters_ThrowsInvalidAirportNameException(string data)
    {
        _ = Assert.Throws<InvalidInputException>(() => _inputValidator.ValidateAircraftData(data));
    }


    [Theory]
    [InlineData("123, Airline", "123, Airline")]
    public void ValidateAirlineData_DuplicateIds_ThrowsDuplicateIdException(params string[] data)
    {
        _ = Assert.Throws<DuplicateIdException>(() => _inputValidator.ValidateAirlineData(data));
    }
}