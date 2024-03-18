using static Airlines.Program;

namespace Airlines.UnitTest;

public class SortingTests
{
    [Fact]
    public void BubbleSort_ShouldSortAirportsCorrectly()
    {
        string[] array = ["aaa", "ccc", "bbb"];
        string[] result = ["aaa", "bbb", "ccc"];

        var sorted = SortAirports(array);

        Assert.Equal(sorted, result);
    }

    [Fact]
    public void SelectionSort_ShouldSortAirlinesCorrectly()
    {
        string[] array = ["aaa", "ccc", "bbb"];
        string[] result = ["aaa", "bbb", "ccc"];

        var sorted = SortAirlines(array);

        Assert.Equal(sorted, result);
    }

    [Fact]
    public void SelectionSort_ShouldSortFlightsCorrectly()
    {
        string[] array = ["aaa", "ccc", "bbb"];
        string[] result = ["aaa", "bbb", "ccc"];

        var sorted = SortFlights(array);

        Assert.Equal(sorted, result);
    }
}
