
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
    public void Add_Airport_With_Long_Code_Fails()
    {
        var airportManager = new AirportManager();
        var longAirportCode = "TooLongAirportCode";

        airportManager.Add(longAirportCode);

        Assert.Empty(airportManager.Airports);
    }

    [Fact]
    public void Add_Airport_With_Duplicate_Code_Fails()
    {
        var airportManager = new AirportManager();
        var airportCode = "ABC";
        airportManager.Add(airportCode);

        airportManager.Add(airportCode);

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

        airportManager.Print();
        var output = writer.ToString().Trim();

        Assert.Contains("AAA", output);
        Assert.Contains("BBB", output);
        Assert.Contains("CCC", output);
    }
}
