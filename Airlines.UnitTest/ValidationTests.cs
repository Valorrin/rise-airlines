using static Airlines.Program;

namespace Airlines.UnitTest;

public class ValidationTests
{
    // Generic value validation tests
    [Fact]
    public void Validate_ShouldReturnTrue_WhenValueIsValidAndNotInArray()
    {
        var value = "John";
        string[] values = ["Alice", "Bob", "Charlie"];

        var result = Validate(value, values);

        Assert.True(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenValueIsNull()
    {
        string? value = null;
        string[] values = ["Alice", "Bob", "Charlie"];

        var result = Validate(value, values);

        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenValueIsEmpty()
    {
        var value = "";
        string[] values = ["Alice", "Bob", "Charlie"];

        var result = Validate(value, values);

        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenValueExistsInArray()
    {
        var value = "Bob";
        string[] values = ["Alice", "Bob", "Charlie"];

        var result = Validate(value, values);

        Assert.False(result);
    }


    // Airport validation tests
    [Fact]
    public void ValidateAirport_ShouldReturnTrue_WhenValidAirport()
    {
        string[] airports = ["abc", "ccc"];

        var result = ValidateAirport("new", airports);

        Assert.True(result);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("abcd")]
    [InlineData("")]
    public void ValidateAirport_ShouldReturnFalse_WhenInvalidAirport(string airport)
    {
        string[] airports = ["abc", "ccc"];

        var result = ValidateAirport(airport, airports);

        Assert.False(result);
    }


    // Airline validation tests
    [Theory]
    [InlineData("12345")]
    [InlineData("abcde")]
    [InlineData("ab12")]
    public void ValidateAirline_ShouldReturnTrue_WhenValidAirline(string airline)
    {
        string[] airlines = ["air1", "air2"];

        var result = ValidateAirline(airline, airlines);

        Assert.True(result);
    }

    [Theory]
    [InlineData("123456")]
    [InlineData("abcdefg")]
    [InlineData("")]
    public void ValidateAirline_ShouldReturnFalse_WhenInvalidAirline(string airline)
    {
        string[] airlines = ["air1", "air2"];

        var result = ValidateAirline(airline, airlines);

        Assert.False(result);
    }


    // Flight validation tests
    [Theory]
    [InlineData("newflight")]
    [InlineData("123456")]
    public void ValidateFlight_ShouldReturnTrue_WhenValidFlight(string flight)
    {
        string[] flights = ["flight1", "flight2"];

        var result = ValidateFlight(flight, flights);

        Assert.True(result);
    }

    [Theory]
    [InlineData("???")]
    [InlineData("?a1")]
    [InlineData("")]
    public void ValidateFlight_ShouldReturnFalse_WhenInvalidFlight(string flight)
    {
        string[] flights = ["flight1", "flight2"];

        var result = ValidateFlight(flight, flights);

        Assert.False(result);
    }
}
