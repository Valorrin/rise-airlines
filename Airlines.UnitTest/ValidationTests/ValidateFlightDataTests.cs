﻿using Airlines.Business;
using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;

namespace Airlines.UnitTests.ConsoleTests;

[Collection("Sequential")]
public class ValidateFlightDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly InputValidator _inputValidator;
    private readonly ConsoleLogger _logger;

    public ValidateFlightDataTests()
    {
        _logger = new ConsoleLogger();
        _airportManager = new AirportManager(_logger);
        _airlineManager = new AirlineManager(_logger);
        _flightManager = new FlightManager(_logger);
        _inputValidator = new InputValidator(
            _airportManager,
            _airlineManager,
            _flightManager
        );
    }

    [Theory]
    [InlineData("JFK, DEP, ARR")] // Valid data
    [InlineData("123, DEP, ARR")] // Valid data
    [InlineData("1234, DEP, ARR")] // Valid data
    public void ValidateFlightData_ValidData_NoExceptionThrown(string data)
    {
        try
        {
            _inputValidator.ValidateFlightData(data);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception: {ex.Message}");
        }
    }

    [Fact]
    public void ValidateFlightData_EmptyData_ThrowsInvalidInputException()
    {
        var data = "";

        _ = Assert.Throws<InvalidInputException>(() => _inputValidator.ValidateFlightData(data));
    }

    [Fact]
    public void ValidateFlightData_DuplicateId_ThrowsDuplicateIdException()
    {
        var data = "1, DEP, ARR ";
        _flightManager.Flights.Add(new Flight() { Id = "1", DepartureAirport = "DEP", ArrivalAirport = "ARR", Duration = 10, Price = 5.5 });

        _ = Assert.Throws<DuplicateIdException>(() => _inputValidator.ValidateFlightData(data));
    }

    [Theory]
    [InlineData("444, DEPPP, ARR ")]
    [InlineData("333, DEP, ARRRRR")]
    [InlineData("222, D, ARR ")]
    [InlineData("111, DEP, A")]
    public void ValidateFlightData_InvalidIdLength_ThrowsInvalidIdLengthException(string data)
    {
        _ = Assert.Throws<InvalidIdLengthException>(() => _inputValidator.ValidateFlightData(data));
    }

    [Theory]
    [InlineData("!@#, DEPP, ARR")] // Invalid characters in ID
    public void ValidateFlightData_InvalidIdCharacters_ThrowsInvalidIdCharactersException(string data)
    {
        _ = Assert.Throws<InvalidIdCharactersException>(() => _inputValidator.ValidateFlightData(data));
    }

    [Theory]
    [InlineData("123, !@#123, ARR")] // Invalid characters in name
    public void ValidateFlightData_InvalidNameCharacters_ThrowsInvalidAirportNameException(string data)
    {
        _ = Assert.Throws<InvalidIdLengthException>(() => _inputValidator.ValidateFlightData(data));
    }

    [Theory]
    [InlineData("123, DEPP, City1@# ")] // Invalid city characters
    public void ValidateFlightData_InvalidCity_ThrowsInvalidAirportCityException(string data)
    {
        _ = Assert.Throws<InvalidIdLengthException>(() => _inputValidator.ValidateFlightData(data));
    }
}