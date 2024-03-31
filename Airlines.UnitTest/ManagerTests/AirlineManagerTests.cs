using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.ManagerTests;

public class AirlineManagerTests
{
    [Fact]
    public void Add_Airline_Successfully()
    {
        var airlineManager = new AirlineManager();
        var airline = new Airline
        {
            Id = "ABC",
            Name = "Test Airline"
        };

        airlineManager.Add(airline);

        Assert.Contains(airline.Id, airlineManager.Airlines.Keys);
        Assert.Contains(airline, airlineManager.Airlines.Values);
    }

    [Theory]
    [InlineData("Test Airline", true)]
    [InlineData("Nonexistent Airline", false)]
    public void Search_Airline_By_Name(string airlineName, bool expectedResult)
    {
        var airlineManager = new AirlineManager();
        var airline = new Airline
        {
            Id = "ABC",
            Name = "Test Airline"
        };
        airlineManager.Add(airline);

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        airlineManager.Search(airlineName);

        var output = writer.ToString().Trim();
        Assert.Equal(expectedResult, output.Contains(airlineName));
    }
}