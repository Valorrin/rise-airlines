using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.UnitTests.ManagerTests;

[Collection("Sequential")]
public class AirportManagerTests
{
    [Fact]
    public void Add_Airport_Successfully()
    {
        var airportManager = new AirportManager();
        var airport = new Airport
        {
            Id = "ABC",
            Name = "Test Airport",
            City = "Test City",
            Country = "Test Country"
        };

        airportManager.Add(airport);

        Assert.Contains(airport, airportManager.Airports);
        Assert.Contains(airport, airportManager.AirportsByCity["Test City"]);
        Assert.Contains(airport, airportManager.AirportsByCountry["Test Country"]);
        Assert.Contains(airport.Name, airportManager.AirportNames);
    }

    [Fact]
    public void Search_Airport_By_Name()
    {
        var airportManager = new AirportManager();
        var airport = new Airport
        {
            Id = "ABC",
            Name = "Test Airport",
            City = "Test City",
            Country = "Test Country"
        };
        airportManager.Add(airport);

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        airportManager.Search("Test Airport");

        var output = writer.ToString().Trim();
        Assert.Contains("Test Airport", output);
    }

    [Theory]
    [InlineData("Test Airport", true)]
    [InlineData("Nonexistent Airport", false)]
    public void Check_Airport_Existence(string airportName, bool expectedResult)
    {
        var airportManager = new AirportManager();
        var airport = new Airport
        {
            Id = "ABC",
            Name = "Test Airport",
            City = "Test City",
            Country = "Test Country"
        };
        airportManager.Add(airport);

        var result = airportManager.Exist(airportName);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void List_Airports_By_City()
    {
    }

    [Fact]
    public void List_Airports_By_Country()
    {
    }
}