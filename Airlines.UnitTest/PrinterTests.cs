

using Airlines.Business;
using Airlines.Console;
using Moq;

namespace Airlines.UnitTests;
public class PrinterTests
{
    [Fact]
    public void Print_AirportManager_PrintsAirports()
    {
        var airportManager = new AirportManager();
        airportManager.Airports.Add("1", new Airport { Name = "Airport1", City = "City1", Country = "Country1" });
        airportManager.Airports.Add("2", new Airport { Name = "Airport2", City = "City2", Country = "Country2" });

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        Printer.Print(airportManager);

        var result = writer.ToString();
        Assert.Contains("Airports:", result);
        Assert.Contains("Airport name: Airport1", result);
        Assert.Contains("Airport city: City1", result);
        Assert.Contains("Airport country: Country1", result);
        Assert.Contains("Airport name: Airport2", result);
        Assert.Contains("Airport city: City2", result);
        Assert.Contains("Airport country: Country2", result);
    }
    [Fact]
    public void Print_AirlineManager_PrintsAirlines()
    {
        var airlineManager = new AirlineManager();
        airlineManager.Airlines.Add("1", new Airline { Name = "Airline1" });
        airlineManager.Airlines.Add("2", new Airline { Name = "Airline2" });

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        Printer.Print(airlineManager);

        var result = writer.ToString();
        Assert.Contains("Airlines:", result);
        Assert.Contains("Airline name: Airline1", result);
        Assert.Contains("Airline name: Airline2", result);
    }

    [Fact]
    public void Print_FlightManager_PrintsFlights()
    {
        var flightManager = new FlightManager();
        flightManager.Flights.Add(new Flight { Id = "Air1" });
        flightManager.Flights.Add(new Flight { Id = "Air2" });

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        Printer.Print(flightManager);

        var result = writer.ToString();
        Assert.Contains("Flights:", result);
        Assert.Contains("Flight ID: Air1", result);
        Assert.Contains("Flight ID: Air2", result);
    }

    [Fact]
    public void PrintAll_PrintsAllManagers()
    {
        var airportManager = new AirportManager();
        airportManager.Airports.Add("123", new Airport { Name = "Airport1", City = "City1", Country = "Country1" });
        airportManager.Airports.Add("223", new Airport { Name = "Airport2", City = "City2", Country = "Country2" });

        var airlineManager = new AirlineManager();
        airlineManager.Airlines.Add("123", new Airline { Name = "Airline1" });
        airlineManager.Airlines.Add("223", new Airline { Name = "Airline2" });

        var flightManager = new FlightManager();
        flightManager.Flights.Add(new Flight { Id = "Air1" });
        flightManager.Flights.Add(new Flight { Id = "Air2" });

        var writer = new StringWriter();
        System.Console.SetOut(writer);

        Printer.PrintAll(airportManager, airlineManager, flightManager);

        var result = writer.ToString();
        Assert.Contains("Airports:", result);
        Assert.Contains("Airport name: Airport1", result);
        Assert.Contains("Airport city: City1", result);
        Assert.Contains("Airport country: Country1", result);
        Assert.Contains("Airport name: Airport2", result);
        Assert.Contains("Airport city: City2", result);
        Assert.Contains("Airport country: Country2", result);
        Assert.Contains("Airlines:", result);
        Assert.Contains("Airline name: Airline1", result);
        Assert.Contains("Airline name: Airline2", result);
        Assert.Contains("Flights:", result);
        Assert.Contains("Flight ID: Air1", result);
        Assert.Contains("Flight ID: Air2", result);
    }
}