
using Airlines.Business.Managers;
using Airlines.Business.Models;

namespace Airlines.Console.Utilities;
public class Validator
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;

    public Validator(AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager)
    {
        _airportManager = airportManager;
        _airlineManager = airlineManager;
        _flightManager = flightManager;
    }

    public bool ValidateAirportData(string data)
    {
        var dataParts = data.Split(", ").ToArray();
        var id = dataParts[0];
        var name = dataParts[1];
        var city = dataParts[2];
        var country = dataParts[3];

        if (string.IsNullOrEmpty(id))
        {
            return false;
        }

        if (_airportManager.Airports.ContainsKey(id))
        {
            System.Console.WriteLine(" Error: An airport with the same ID already exists.");
            return false;
        }

        if (id.Length is < 2 or > 4)
        {
            return false;
        }

        foreach (var c in id)
        {
            if (!char.IsLetterOrDigit(c))
            {
                return false;
            }
        }

        foreach (var c in name)
        {
            if (!char.IsLetter(c) || c == ' ')
            {
                return false;
            }
        }

        foreach (var c in city)
        {
            if (!char.IsLetter(c) || c == ' ')
            {
                return false;
            }
        }

        foreach (var c in country)
        {
            if (!char.IsLetter(c) || c == ' ')
            {
                return false;
            }
        }

        return true;

    }
}
