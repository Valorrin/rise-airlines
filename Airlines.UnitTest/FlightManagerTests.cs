using Airlines.Business;

namespace Airlines.Console.Tests;

public class FlightManagerTests
{
    [Fact]
    public void Add_Flight_Successfully()
    {
        var flightManager = new FlightManager();
        var flightNumber = "ABC123";

        flightManager.Add(flightNumber);

        Assert.Contains(flightNumber, flightManager.Flights);
    }

    [Fact]
    public void Add_Flight_With_Duplicate_Number_Fails()
    {
        var flightManager = new FlightManager();
        var flightNumber = "ABC123";
        flightManager.Add(flightNumber);

        flightManager.Add(flightNumber);

        _ = Assert.Single(flightManager.Flights);
    }

    [Theory]
    [InlineData("ABC12", true)]
    [InlineData("NonexistentFlight", false)]
    public void Search_Flight(string searchTerm, bool expectedResult)
    {
        var flightManager = new FlightManager();
        flightManager.Add("ABC12");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        flightManager.Search(searchTerm);
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
    public void Print_Flights()
    {
        var flightManager = new FlightManager();
        flightManager.Add("AAA");
        flightManager.Add("BBB");
        flightManager.Add("CCC");

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        Printer.Print(flightManager);
        var output = writer.ToString().Trim();

        Assert.Contains("AAA", output);
        Assert.Contains("BBB", output);
        Assert.Contains("CCC", output);
    }

    [Theory]
    [InlineData("ABC123", true)]
    [InlineData("DEFDEF", true)]
    [InlineData("123456", true)]
    [InlineData("GHI#789", false)]
    public void Validate_FlightName(string name, bool expectedResult)
    {
        var flightManager = new FlightManager();

        var result = flightManager.Validate(name);

        Assert.Equal(expectedResult, result);
    }
}
