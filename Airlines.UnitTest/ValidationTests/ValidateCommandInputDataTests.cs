using Airlines.Business.Managers;
using Airlines.Console;
using Airlines.Console.Exceptions;

namespace Airlines.UnitTests.ConsoleTests;

[Collection("Sequential")]
public class ValidateCommandInputDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;
    private readonly RouteManager _routeManager;
    private readonly InputValidator _inputValidator;

    public ValidateCommandInputDataTests()
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
    [InlineData("search query")] // Valid search command
    [InlineData("sort airports")] // Valid sort command
    [InlineData("exist something")] // Valid exist command
    [InlineData("list a b ")] // Valid list command
    [InlineData("route new")] // Valid route command
    [InlineData("batch start")] // Valid batch command
    [InlineData("batch run")]
    [InlineData("batch cancel")]
    public void ValidateCommandInputData_ValidCommands_NoExceptionThrown(string data)
    {
        try
        {
            _inputValidator.ValidateCommandInputData(data);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception: {ex.Message}");
        }
    }

    [Theory]
    [InlineData("wrongCommand")]
    [InlineData("sort")]
    [InlineData("")]
    public void ValidateCommandInputData_InvalidCommandArguments_ThrowsInvalidCommandException(string data)
    {
        _ = Assert.Throws<InvalidCommandException>(() => _inputValidator.ValidateCommandInputData(data));
    }

    [Theory]
    [InlineData("sort something")]
    [InlineData("route something")]
    public void ValidateCommandInputData_InvalidCommandArguments_ThrowsInvalidCommandArgumentException(string data)
    {
        _ = Assert.Throws<InvalidCommandArgumentException>(() => _inputValidator.ValidateCommandInputData(data));
    }

    [Theory]
    [InlineData("reserve 1 1 ")]
    [InlineData("reserve cargo 1 ")]
    [InlineData("reserve ticket 1 ")]
    [InlineData("list a")]

    public void ValidateCommandInputData_InvalidCommandArguments_ThrowsInvalidNumberOfArgumentsException(string data)
    {
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _inputValidator.ValidateCommandInputData(data));
    }

    [Fact]
    public void ValidateCommandInputData_InvalidNumberOfArguments_ThrowsInvalidNumberOfArgumentsException()
    {
        var invalidCommand = "route find";

        var exception = Assert.Throws<InvalidNumberOfArgumentsException>(() => _inputValidator.ValidateCommandInputData(invalidCommand));
        Assert.Equal("Incorrect command format. Please use the following format: route find <Departure Airport ID> <Arrival Airport>", exception.Message);
    }

    [Fact]
    public void ValidateCommandInputData_DepartureSameAsArrival_ThrowsInvalidCommandArgumentException()
    {
        var invalidCommand = "route find airport1 airport1";

        var exception = Assert.Throws<InvalidCommandArgumentException>(() => _inputValidator.ValidateCommandInputData(invalidCommand));
        Assert.Equal("Departure airport cannot be the same as the Arrival airport!", exception.Message);
    }

    [Fact]
    public void ValidateCommandInputData_NonExistentDepartureAirport_ThrowsInvalidCommandArgumentException()
    {
        var invalidCommand = "route find nonExistentAirport airport2";

        var exception = Assert.Throws<InvalidCommandArgumentException>(() => _inputValidator.ValidateCommandInputData(invalidCommand));
        Assert.Equal("Departure airport does not exist!", exception.Message);
    }
}