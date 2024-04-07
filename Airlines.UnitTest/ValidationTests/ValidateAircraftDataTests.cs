using Airlines.Business;
using Airlines.Business.Exceptions;
using Airlines.Business.Managers;

namespace Airlines.UnitTests.ValidationTests;

[Collection("Sequential")]
public class ValidateAircraftDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly InputValidator _inputValidator;

    public ValidateAircraftDataTests()
    {
        _airportManager = new AirportManager();
        _airlineManager = new AirlineManager();
        _flightManager = new FlightManager();
        _inputValidator = new InputValidator(
            _airportManager,
            _airlineManager,
            _flightManager
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
}