using static Airlines.Program;

namespace Airlines.UnitTest
{
    public class ValidationTests
    {
        [Fact]
        public void ValidateAirportShouldReturnTrue()
        {
            string[] airports = { "abc", "ccc" };

            bool result = ValidateAirport("new", airports);

            Assert.True(result);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("abcd")]
        [InlineData("")]
        public void ValidateAirportShouldReturnFalse(string airport)
        {
            string[] airports = { "abc", "ccc" };

            bool result = ValidateAirport(airport, airports);

            Assert.False(result);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("abcde")]
        [InlineData("ab12")]
        public void ValidateAirlineShoudReturnTrue(string airline)
        {
            string[] airlines = { "air1", "air2" };

            bool result = ValidateAirline(airline, airlines);

            Assert.True(result);
        }

        [Theory]
        [InlineData("123456")]
        [InlineData("abcdefg")]
        [InlineData("")]
        public void ValidateAirlineShoudReturnFalse(string airline)
        {
            string[] airlines = { "air1", "air2" };

            bool result = ValidateAirline(airline, airlines);

            Assert.False(result);
        }

        [Theory]
        [InlineData("newflight")]
        [InlineData("123456")]
        public void ValidateFlightShoudReturnTrue(string flight)
        {
            string[] flights = { "flight1", "flight2" };

            bool result = ValidateFlight(flight, flights);

            Assert.True(result);
        }

        [Theory]
        [InlineData("???")]
        [InlineData("?a1")]
        [InlineData("")]
        public void ValidateFlightShoudReturnFalse(string flight)
        {
            string[] flights = { "flight1", "flight2" };

            bool result = ValidateFlight(flight, flights);

            Assert.False(result);
        }
    }
}
