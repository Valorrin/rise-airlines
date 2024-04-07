using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
using Airlines.Business.Validation;


namespace Airlines.UnitTests.ConsoleTests;

[Collection("Sequential")]
public class ValidateCommandInputDataTests
{
    private readonly AirportManager _airportManager;
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;
    private readonly RouteManager _routeManager;
    private readonly CommandValidator _commandValidator;

    public ValidateCommandInputDataTests()
    {
        _airportManager = new AirportManager();
        _flightManager = new FlightManager();
        _aircraftManager = new AircraftManager();
        _routeManager = new RouteManager(_airportManager);

        _commandValidator = new CommandValidator(
            _airportManager,
            _flightManager,
            _aircraftManager,
            _routeManager
        );
    }

    [Theory]
    [InlineData("search query")] // Valid search command
    [InlineData("sort airports ascending")] // Valid sort command
    [InlineData("sort airports descending")] // Valid sort command
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
            _commandValidator.ValidateCommandArguments(data);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception: {ex.Message}");
        }
    }
}