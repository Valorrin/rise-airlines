using Airlines.Business;

namespace Airlines.Console.Tests;

public class AirportManagerTests
{
    [Fact]
    public void Add_Airport_Successfully()
    {
        var airportManager = new AirportManager();
        var airportCode = "ABC";

        airportManager.Add(airportCode);

        Assert.Contains(airportCode, airportManager.Airports);
    }

    [Fact]
    public void Add_Airport_With_Long_Name_Fails()
    {
        var airportManager = new AirportManager();
        var longAirportName = "TooLongAirportCode";

        if (airportManager.Validate(longAirportName))
        {
            airportManager.Add(longAirportName);
        }

        if (airportManager.Validate(longAirportName))
        {
            airportManager.Add(longAirportName);
        }

        Assert.Empty(airportManager.Airports);
    }

    [Fact]
    public void Add_Airport_With_Duplicate_Name_Fails()
    {
        var airportManager = new AirportManager();
        var airportName = "ABC";

        if (airportManager.Validate(airportName))
        {
            airportManager.Add(airportName);
        }

        if (airportManager.Validate(airportName))
        {
            airportManager.Add(airportName);
        }

        _ = Assert.Single(airportManager.Airports);
    }

    [Theory]
    [InlineData("ABC", true)]
    [InlineData("NonexistentAirport", false)]
    public void Search_Airport(string searchTerm, bool expectedResult)
    {
        var airportManager = new AirportManager();
        airportManager.Add("ABC");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        airportManager.Search(searchTerm);
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

    [Fact]
    public void Print_Airports()
    {
        var airportManager = new AirportManager();
        airportManager.Add("AAA");
        airportManager.Add("BBB");
        airportManager.Add("CCC");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        Printer.Print(airportManager);
        var output = writer.ToString().Trim();

        Assert.Contains("AAA", output);
        Assert.Contains("BBB", output);
        Assert.Contains("CCC", output);
    }

    [Theory]
    [InlineData("AAA", true)]
    [InlineData("CCCC", false)]
    [InlineData("12$", false)]
    [InlineData("", false)]
    [InlineData("A", false)]
    public void Validate_AirportName(string name, bool expectedResult)
    {
        var airportManager = new AirportManager();

        var result = airportManager.Validate(name);

        Assert.Equal(expectedResult, result);
    }
}
