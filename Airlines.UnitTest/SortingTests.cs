using static Airlines.Program;

namespace Airlines.UnitTest
{
    public class SortingTests
    {
        [Fact]
        public void AirportBoubleSortShouldWork()
        {
            string[] array = ["aaa", "ccc", "bbb"];
            string[] result = ["aaa", "bbb", "ccc"];

            string[] sorted = SortAirports(array);

            Assert.Equal(sorted, result);
        }

        [Fact]
        public void AirlineSelectionSortShouldWork() 
        {
            string[] array = ["aaa", "ccc", "bbb"];
            string[] result = ["aaa", "bbb", "ccc"];

            string[] sorted = SortAirports(array);

            Assert.Equal(sorted, result);
        }

        [Fact]
        public void FlightSelectionSortShouldWork()
        {
            string[] array = ["aaa", "ccc", "bbb"];
            string[] result = ["aaa", "bbb", "ccc"];

            string[] sorted = SortAirports(array);

            Assert.Equal(sorted, result);
        }

    }
}
