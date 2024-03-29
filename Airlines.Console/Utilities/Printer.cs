using Airlines.Business.Managers;

namespace Airlines.Console.Utilities;
public class Printer
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;

    public Printer(AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager, AircraftManager aircraftManager)
    {
        _airportManager = airportManager;
        _airlineManager = airlineManager;
        _flightManager = flightManager;
        _aircraftManager = aircraftManager;
    }

    public void PrintAirports()
    {
        System.Console.Write($"Airports:\n");
        foreach (var airport in _airportManager.Airports.Values)
        {
            System.Console.WriteLine($" Airport name: {airport.Name}");
            System.Console.WriteLine($" Airport city: {airport.City}");
            System.Console.WriteLine($" Airport country: {airport.Country}\n");
        }
    }
    public void PrintAirlines()
    {
        System.Console.Write($"Airlines:\n");
        foreach (var airline in _airlineManager.Airlines.Values)
            System.Console.WriteLine($" Airline name: {airline.Name}\n");
    }
    public void PrintFlights()
    {
        System.Console.Write($"Flights: \n");
        foreach (var flight in _flightManager.Flights)
        {
            System.Console.WriteLine($" Flight ID: {flight.Id}");
            System.Console.WriteLine($" Aircraft Model: {flight.AircraftModel}");
        }
    }
    public void PrintAircrafts()
    {
        System.Console.Write($"\nAircrafts: \n");
        foreach (var aircraft in _aircraftManager.CargoAircrafts)
            System.Console.WriteLine($" Cargo Aircraft: {aircraft.Model}");
        foreach (var aircraft in _aircraftManager.PassengerAircrafts)
            System.Console.WriteLine($" Passenger Aircraft: {aircraft.Model}");
        foreach (var aircraft in _aircraftManager.PrivateAircrafts)
            System.Console.WriteLine($" Private Aircraft: {aircraft.Model}");
    }
    public void PrintAll()
    {
        PrintAircrafts();
        PrintAirlines();
        PrintFlights();
        PrintAircrafts();
    }
}