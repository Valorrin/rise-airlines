using System;
using System.Collections.Generic;
using System.IO;
using Airlines.Business.Managers;
using Airlines.Console;


namespace Airlines.UnitTests.ConsoleTests
{
    public class InputValidatorTests
    {
        private readonly AirportManager _airportManager;
        private readonly AirlineManager _airlineManager;
        private readonly FlightManager _flightManager;
        private readonly AircraftManager _aircraftManager;
        private readonly RouteManager _routeManager;

        private readonly InputValidator _inputValidator;

        public InputValidatorTests()
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
        [InlineData("JFK, John F Kennedy International Airport, New York, USA")] // Valid data
        [InlineData("123, AirportA, City, Country")] // Valid data
        [InlineData("1234, AirportB, City, Country")] // Valid data
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
    }
}
