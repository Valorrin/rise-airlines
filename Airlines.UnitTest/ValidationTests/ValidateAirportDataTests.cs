using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Console;
using Airlines.Console.Exceptions;


namespace Airlines.UnitTests.ConsoleTests
{
    public class InputValidatorTests
    {
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
            _airportManager.Airports.Add("1", new Airport() { Id = "1", Name = "Airport1", City = "City1", Country = "Country1" });

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

        [Theory]
        [InlineData("123, Airport, City1@#, Country")] // Invalid city characters
        public void ValidateAirportData_InvalidCity_ThrowsInvalidAirportCityException(string data)
        {
            _ = Assert.Throws<InvalidAirportCityException>(() => _inputValidator.ValidateAirportData(data));
        }

        [Theory]
        [InlineData("123, Airport, City, Country 1@#")] // Invalid country characters
        public void ValidateAirportData_InvalidCountry_ThrowsInvalidAirportCountryException(string data)
        {
            _ = Assert.Throws<InvalidAirportCountryException>(() => _inputValidator.ValidateAirportData(data));
        }
    }
}