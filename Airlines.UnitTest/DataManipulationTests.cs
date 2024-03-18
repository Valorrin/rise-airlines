using static Airlines.Program;

namespace Airlines.UnitTests;

public class DataManipulationTests
{
    [Fact]
    public void AddData_ShouldAddItemToEmptyArray()
    {
        var item = "abc";
        var data = new string[1];
        var expected = new string[] { "abc" };

        var result = AddData(item, data);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddData_ShouldAddItemToNonEmptyArray()
    {
        var item = "def";
        var data = new string[2];
        data[0] = "abc";
        var expected = new string[] { "abc", "def" };

        var result = AddData(item, data);

        Assert.Equal(expected, result);
    }
}
