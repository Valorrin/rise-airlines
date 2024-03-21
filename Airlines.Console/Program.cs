using Airlines.Business;
using System;
using System.Runtime.InteropServices;
using static Airlines.Console.InputReader;
using static Airlines.Console.Printer;

namespace Airlines;
public class Program
{
    public static void Main()
    {
        var airportManager = new AirportManager();
        var airlineManager = new AirlineManager();
        var flightManager = new FlightManager();

        var currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        var airportFilePath = Path.Combine(currentDirectory, @"..\..\..\Data\airports.csv");
        var airlineFilePath = Path.Combine(currentDirectory, @"..\..\..\Data\airlines.csv");

        var airportData = ReadFromFile(airportFilePath);
        var airlineData = ReadFromFile(airlineFilePath);

        airportManager.Add(airportData);

        ReadInput(flightManager);

        Print(airportManager);
        Print(airlineManager);
        Print(flightManager);

        _ = ReadCommands(airportManager, airlineManager, flightManager);
    }
}