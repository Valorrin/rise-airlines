using Airlines.Business.Managers;
using Airlines.Console;
using Airlines.Console.Exceptions;

namespace Airlines.UnitTests.ConsoleTests;

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
    public void ValidateCommandInputData_EmptyRouteForRemoveCommand_ThrowsEmptyRouteException()
    {
        var data = "route remove";
        _routeManager.Routes.Clear();

        _ = Assert.Throws<EmptyRouteException>(() => _inputValidator.ValidateCommandInputData(data));
    }
}