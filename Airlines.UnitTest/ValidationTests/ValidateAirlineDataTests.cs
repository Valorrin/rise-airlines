using Airlines.Business;
using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;

namespace Airlines.UnitTests.ConsoleTests;

[Collection("Sequential")]
public class ValidateAirlineDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly InputValidator _inputValidator;
    private readonly ConsoleLogger _consoleLogger;

    public ValidateAirlineDataTests()
    {
        _consoleLogger = new ConsoleLogger();
        _airportManager = new AirportManager(_consoleLogger);
        _airlineManager = new AirlineManager(_consoleLogger);
        _flightManager = new FlightManager(_consoleLogger);
        _inputValidator = new InputValidator(
            _airportManager,
            _airlineManager,
            _flightManager
        );
    }

    [Theory]
    [InlineData("JFK, John")] // Valid data
    [InlineData("123, AirlineA")] // Valid data
    [InlineData("1234, AirlineB")] // Valid data
    public void ValidateAirlineData_ValidData_NoExceptionThrown(string data)
    {
        try
        {
            _inputValidator.ValidateAirlineData(data);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception: {ex.Message}");
        }
    }

    [Fact]
    public void ValidateAirlineData_EmptyData_ThrowsInvalidInputException()
    {
        var data = "";

        _ = Assert.Throws<InvalidInputException>(() => _inputValidator.ValidateAirlineData(data));
    }

    [Fact]
    public void ValidateAirlineData_DuplicateId_ThrowsDuplicateIdException()
    {
        var data = "123, Airline";
        _airlineManager.Airlines.Add(new Airline() { Id = "123", Name = "Airline" });

        _ = Assert.Throws<DuplicateIdException>(() => _inputValidator.ValidateAirlineData(data));
    }

    [Theory]
    [InlineData("1, A ")] // ID length < 2
    [InlineData("12345, Airport ")] // ID length > 4
    public void ValidateAirlineData_InvalidIdLength_ThrowsInvalidIdLengthException(string data)
    {
        _ = Assert.Throws<InvalidIdLengthException>(() => _inputValidator.ValidateAirlineData(data));
    }

    [Theory]
    [InlineData("!@#, Airline ")] // Invalid characters in ID
    public void ValidateAirlineData_InvalidIdCharacters_ThrowsInvalidIdCharactersException(string data)
    {
        _ = Assert.Throws<InvalidIdCharactersException>(() => _inputValidator.ValidateAirlineData(data));
    }

    [Theory]
    [InlineData("123, !@#123 ")] // Invalid characters in name
    public void ValidateAirlineData_InvalidNameCharacters_ThrowsInvalidAirportNameException(string data)
    {
        _ = Assert.Throws<InvalidAirlineNameException>(() => _inputValidator.ValidateAirlineData(data));
    }
}