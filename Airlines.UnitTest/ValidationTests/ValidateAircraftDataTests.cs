using Airlines.Business;
using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
using Airlines.Business.Utilities;

namespace Airlines.UnitTests.ValidationTests;

[Collection("Sequential")]
public class ValidateAircraftDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly InputValidator _inputValidator;
    private readonly ConsoleLogger _consoleLogger;

    public ValidateAircraftDataTests()
    {
        _consoleLogger = new ConsoleLogger();
        _airportManager = new AirportManager(_consoleLogger);
        _airlineManager = new AirlineManager(_consoleLogger);
        _flightManager = new FlightManager(_consoleLogger);
        var inputValidator = new InputValidator(
                    _airportManager,
                    _airlineManager,
                    _flightManager
                );
        _inputValidator = inputValidator;
    }

    [Theory]
    [InlineData("JFK")]
    [InlineData("123")]
    [InlineData("12AD")]
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
}