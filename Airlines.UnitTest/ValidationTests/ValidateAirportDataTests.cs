﻿using Airlines.Business;
using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Utilities;

namespace Airlines.UnitTests.ConsoleTests;

[Collection("Sequential")]
public class ValidateAirportDataTests
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly InputValidator _inputValidator;
    private readonly ConsoleLogger _logger;

    public ValidateAirportDataTests()
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
    [InlineData("JFK, John F Kennedy International Airport, New York, USA")]
    [InlineData("123, AirportA, City, Country")]
    [InlineData("1234, AirportB, City, Country")]
    public void ValidateAirportData_ValidData_NoExceptionThrown(string data)
    {
        try
        {
            _inputValidator.ValidateAirportData(data);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception: {ex.Message}");
        }
    }

    [Fact]
    public void ValidateAirportData_EmptyData_ThrowsInvalidInputException()
    {
        var data = "";

        _ = Assert.Throws<InvalidInputException>(() => _inputValidator.ValidateAirportData(data));
    }

    [Fact]
    public void ValidateAirportData_DuplicateId_ThrowsDuplicateIdException()
    {
        var data = "1, Airport1, City1, Country1";
        _airportManager.Airports.Add(new Airport() { Id = "1", Name = "Airport1", City = "City1", Country = "Country1" });

        _ = Assert.Throws<DuplicateIdException>(() => _inputValidator.ValidateAirportData(data));
    }

    [Theory]
    [InlineData("1, A, City1, Country1")] // ID length < 2
    [InlineData("12345, Airport1, City1, Country1")] // ID length > 4
    public void ValidateAirportData_InvalidIdLength_ThrowsInvalidIdLengthException(string data)
    {
        _ = Assert.Throws<InvalidIdLengthException>(() => _inputValidator.ValidateAirportData(data));
    }

    [Theory]
    [InlineData("!@#, Airport, City, Country")] // Invalid characters in ID
    public void ValidateAirportData_InvalidIdCharacters_ThrowsInvalidIdCharactersException(string data)
    {
        _ = Assert.Throws<InvalidIdCharactersException>(() => _inputValidator.ValidateAirportData(data));
    }

    [Theory]
    [InlineData("123, !@#123, City1, Country1")] // Invalid characters in name
    public void ValidateAirportData_InvalidNameCharacters_ThrowsInvalidAirportNameException(string data)
    {
        _ = Assert.Throws<InvalidAirportNameException>(() => _inputValidator.ValidateAirportData(data));
    }
}