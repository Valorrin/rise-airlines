using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
using Airlines.Business.Utilities;
using Airlines.Business.Validation;


namespace Airlines.UnitTests.ValidationTests;

[Collection("Sequential")]
public class ValidateCommandIArgumentTests
{
    private readonly AirportManager _airportManager;
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;
    private readonly RouteManager _routeManager;
    private readonly CommandValidator _commandValidator;
    private readonly ConsoleLogger _consoleLogger;


    public ValidateCommandIArgumentTests()
    {
        _consoleLogger = new ConsoleLogger();
        _airportManager = new AirportManager(_consoleLogger);
        _flightManager = new FlightManager(_consoleLogger);
        _aircraftManager = new AircraftManager();
        _routeManager = new RouteManager(_airportManager, _consoleLogger);

        _commandValidator = new CommandValidator(
            _airportManager,
            _flightManager,
            _aircraftManager,
            _routeManager
        );
    }

    [Theory]
    [InlineData("search arg1")]
    [InlineData("sort arg1 arg2")]
    [InlineData("exist arg1")]
    [InlineData("list arg1 arg2")]
    [InlineData("route add arg1")]
    [InlineData("route check arg1 arg2")]
    [InlineData("route search arg1 arg2 aeg3")]
    [InlineData("reserve cargo arg1 arg2 arg3")]
    [InlineData("reserve ticket arg1 arg2 arg3 arg4")]
    [InlineData("batch new")]
    [InlineData("batch run")]
    [InlineData("batch cancel")]
    public void ValidateCommandArguments_ValidInputs_NoExceptionThrown(string commandInput)
    {
        _commandValidator.ValidateCommandArguments(commandInput);

        Assert.True(true);
    }

    [Fact]
    public void ValidateCommandArguments_InvalidCommand_ThrowsInvalidCommandException()
    {
        var commandInput = "invalidCommand";
        _ = Assert.Throws<InvalidCommandException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }

    [Fact]
    public void ValidateCommandArguments_InvalidNumberOfArgumentsForSort_ThrowsInvalidNumberOfArgumentsException()
    {
        var commandInput = "sort arg1";
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }

    [Fact]
    public void ValidateCommandArguments_InvalidNumberOfArgumentsForExist_ThrowsInvalidNumberOfArgumentsException()
    {
        var commandInput = "exist arg1 arg2";
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }

    [Fact]
    public void ValidateCommandArguments_InvalidNumberOfArgumentsForList_ThrowsInvalidNumberOfArgumentsException()
    {
        var commandInput = "list arg1";
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }

    [Fact]
    public void ValidateCommandArguments_InvalidNumberOfArgumentsForRouteAdd_ThrowsInvalidNumberOfArgumentsException()
    {
        var commandInput = "route add arg1 arg 2";
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }

    [Fact]
    public void ValidateCommandArguments_InvalidNumberOfArgumentsForRouteCheck_ThrowsInvalidNumberOfArgumentsException()
    {
        var commandInput = "route check arg1";
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }

    [Fact]
    public void ValidateCommandArguments_InvalidNumberOfArgumentsForRouteSearch_ThrowsInvalidNumberOfArgumentsException()
    {
        var commandInput = "route search arg1";
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }

    [Fact]
    public void ValidateCommandArguments_InvalidNumberOfArgumentsForReserveCargo_ThrowsInvalidNumberOfArgumentsException()
    {
        var commandInput = "reserve cargo arg1 arg2 arg3 arg4";
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }

    [Fact]
    public void ValidateCommandArguments_InvalidNumberOfArgumentsForReserveTicket_ThrowsInvalidNumberOfArgumentsException()
    {
        var commandInput = "reserve ticket arg1 arg2 arg3";
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }

    [Theory]
    [InlineData("batch arg1 arg2")]
    [InlineData("batch invalid")]
    public void ValidateCommandArguments_InvalidNumberOfArgumentsForBatchInvalid_ThrowsInvalidNumberOfArgumentsException(string commandInput)
    {
        commandInput = "batch invalid invalid";
        _ = Assert.Throws<InvalidNumberOfArgumentsException>(() => _commandValidator.ValidateCommandArguments(commandInput));
    }
}