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

        [Fact]
        public void ValidateAirportShouldReturnFalse()
        {
            string[] airports = { "abc", "ccc" };

            bool result = ValidateAirport("ndew", airports);

            Assert.False(result);
        }

        [Fact]
        public void ValidateAirlineShoudReturnTrue()
        {
            string[] airlines = { "air1", "air2" };

            bool result = ValidateAirline("newnw", airlines);

            Assert.True(result);
        }

        [Fact]
        public void ValidateAirlineShoudReturnFalse()
        {
            string[] airlines = { "air1", "air2" };

            bool result = ValidateAirline("newnewnew", airlines);

            Assert.False(result);
        }

        [Fact]
        public void ValidateFlightShoudReturnTrue()
        {
            string[] flights = { "flight1", "flight2" };

            bool result = ValidateFlight("newflight", flights);

            Assert.True(result);
        }

        [Fact]
        public void ValidateFlightShoudReturnFalse()
        {
            string[] flights = { "flight1", "flight2" };

            bool result = ValidateFlight("???", flights);

            Assert.False(result);
        }
    }
}
