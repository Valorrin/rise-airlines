using Airlines.Business;

namespace Airlines.UnitTests;
public class SearchTests
{
    [Fact]
    public void LinearSearch_Finds_Existing_Element()
    {
        var data = new List<string> { "apple", "banana", "cherry", "date" };
        var target = "banana";

        var index = Search.LinearSearch(data, target);

        Assert.Equal(1, index);
    }

    [Fact]
    public void LinearSearch_Returns_Minus_One_For_Nonexistent_Element()
    {
        var data = new List<string> { "apple", "banana", "cherry", "date" };
        var target = "grape";

        var index = Search.LinearSearch(data, target);

        Assert.Equal(-1, index);
    }

    [Fact]
    public void BinarySearch_Finds_Existing_Element()
    {
        var data = new List<string> { "apple", "banana", "cherry", "date" };
        var target = "cherry";

        var index = Search.BinarySearch(data, target);

        Assert.Equal(2, index);
    }

    [Fact]
    public void BinarySearch_Returns_Minus_One_For_Nonexistent_Element()
    {
        var data = new List<string> { "apple", "banana", "cherry", "date" };
        var target = "grape";

        var index = Search.BinarySearch(data, target);

        Assert.Equal(-1, index);
    }

    [Fact]
    public void LinearSearch_Returns_Minus_One_For_Empty_List()
    {
        var data = new List<string>();
        var target = "apple";

        var index = Search.LinearSearch(data, target);

        Assert.Equal(-1, index);
    }

    [Fact]
    public void BinarySearch_Returns_Minus_One_For_Empty_List()
    {
        var data = new List<string>();
        var target = "apple";

        var index = Search.BinarySearch(data, target);

        Assert.Equal(-1, index);
    }
}
