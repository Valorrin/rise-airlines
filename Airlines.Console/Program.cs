using Airlines.Business;
using System;
using static Airlines.Console.InputReader;
using static Airlines.Console.Printer;

namespace Airlines;
public class Program
{
    public static void Main()
    {
        var airports = new AirportManager();
        var airlines = new AirlineManager();
        var flights = new FlightManager();

        var currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        var airportFilePath = Path.Combine(currentDirectory, @"..\..\..\Data\airports.csv");
        var airlineFilePath = Path.Combine(currentDirectory, @"..\..\..\Data\airlines.csv");

        var airportData = ReadFromFile(airportFilePath);
        var airlineData = ReadFromFile(airlineFilePath);

        foreach (var item in airportData)
        {
            System.Console.WriteLine(item);
        }

        ReadInput(airports);
        ReadInput(airlines);
        ReadInput(flights);

        Print(airports);
        Print(airlines);
        Print(flights);

        _ = ReadCommands(airports, airlines, flights);
    }
}