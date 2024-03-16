using static Airlines.Program;

namespace Airlines.UnitTest
{
    public class SortingTests
    {
        [Fact]
        public void BubbleSort_ShouldSortAirportsCorrectly()
        {
            string[] array = ["aaa", "ccc", "bbb"];
            string[] result = ["aaa", "bbb", "ccc"];

            string[] sorted = SortAirports(array);

            Assert.Equal(sorted, result);
        }

        [Fact]
        public void SelectionSort_ShouldSortAirlinesCorrectly() 
        {
            string[] array = ["aaa", "ccc", "bbb"];
            string[] result = ["aaa", "bbb", "ccc"];

            string[] sorted = SortAirlines(array);

            Assert.Equal(sorted, result);
        }

        [Fact]
        public void SelectionSort_ShouldSortFlightsCorrectly()
        {
            string[] array = ["aaa", "ccc", "bbb"];
            string[] result = ["aaa", "bbb", "ccc"];

            string[] sorted = SortFlights(array);

            Assert.Equal(sorted, result);
        }

    }
}
