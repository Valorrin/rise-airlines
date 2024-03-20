using Airlines.Business;

namespace Airlines.Console.Tests;

public class AirlineManagerTests
{
    [Fact]
    public void Add_Airline_Successfully()
    {
        var airlineManager = new AirlineManager();
        var airlineName = "Test";

        airlineManager.Add(airlineName);

        Assert.Contains(airlineName, airlineManager.Airlines);
    }

    [Fact]
    public void Add_Airline_With_Long_Name_Fails()
    {
        var airlineManager = new AirlineManager();
        var longAirlineName = "TooLongAirlineName";

        if (airlineManager.Validate(longAirlineName))
        {
            airlineManager.Add(longAirlineName);
        }

        if (airlineManager.Validate(longAirlineName))
        {
            airlineManager.Add(longAirlineName);
        }

        Assert.Empty(airlineManager.Airlines);
    }

    [Fact]
    public void Add_Airline_With_Duplicate_Name_Fails()
    {
        var airlineManager = new AirlineManager();
        var airlineName = "Test";

        if (airlineManager.Validate(airlineName))
        {
            airlineManager.Add(airlineName);
        }

        if (airlineManager.Validate(airlineName))
        {
            airlineManager.Add(airlineName);
        }

        _ = Assert.Single(airlineManager.Airlines);
    }

    [Theory]
    [InlineData("Test", true)]
    [InlineData("NonexistentAirline", false)]
    public void Search_Airline(string searchTerm, bool expectedResult)
    {
        var airlineManager = new AirlineManager();
        airlineManager.Add("Test");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        airlineManager.Search(searchTerm);
        var output = writer.ToString().Trim();

        if (expectedResult)
        {
            Assert.Contains(searchTerm, output);
        }
        else
        {
            Assert.DoesNotContain(searchTerm, output);
        }
    }

    [Theory]
    [InlineData("abcde", true)]
    [InlineData("12345", true)]
    [InlineData("LongAirlineName", false)]
    [InlineData("123456", false)]
    [InlineData("abc456", false)]
    public void Validate_AirlineName(string name, bool expectedResult)
    {
        var airlineManager = new AirlineManager();

        var result = airlineManager.Validate(name);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Print_Airlines()
    {
        var airlineManager = new AirlineManager();
        airlineManager.Add("AAA");
        airlineManager.Add("BBB");
        airlineManager.Add("CCC");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        Printer.Print(airlineManager);
        var output = writer.ToString().Trim();

        Assert.Contains("AAA", output);
        Assert.Contains("BBB", output);
        Assert.Contains("CCC", output);
    }
}
