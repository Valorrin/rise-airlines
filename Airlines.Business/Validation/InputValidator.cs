using Airlines.Business.Managers;

using Airlines.Business.Exceptions;

namespace Airlines.Business;
public class InputValidator
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;

    public InputValidator(AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager)
    {
        _airportManager = airportManager;
        _airlineManager = airlineManager;
        _flightManager = flightManager;
    }

    public void ValidateAirportData(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new InvalidInputException("Airport data cannot be empty.");
        }

        var dataParts = data.Split(", ").ToArray();
        var id = dataParts.ElementAtOrDefault(0);
        var name = dataParts[1];
        var city = dataParts[2];
        var country = dataParts[3];

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
        {
            throw new InvalidInputException("Airport data cannot be empty.");
        }

        if (_airportManager.Airports.Any(airport => airport.Id == id))
        {
            throw new DuplicateIdException("An airport with the same ID already exists.");
        }

        if (id.Length is < 2 or > 4)
        {
            throw new InvalidIdLengthException("Airport ID must be between 2 and 4 characters.");
        }

        if (!ContainsOnlyLettersOrDigits(id))
        {
            throw new InvalidIdCharactersException("Airport ID contains invalid characters. Only letters and digits are allowed.");
        }

        if (!ContainsOnlyLettersAndSpaces(name))
        {
            throw new InvalidAirportNameException("Airport name contains invalid characters. Only letters and spaces are allowed.");
        }

        if (!ContainsOnlyLettersAndSpaces(city))
        {
            throw new InvalidAirportCityException("Airport city contains invalid characters. Only letters and spaces are allowed.");
        }

        if (!ContainsOnlyLettersAndSpaces(country))
        {
            throw new InvalidAirportCountryException("Airport country contains invalid characters. Only letters and spaces are allowed.");
        }
    }

    public void ValidateAirlineData(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new InvalidInputException("Airline data cannot be empty.");
        }

        var dataParts = data.Split(", ").ToArray();
        var id = dataParts[0];
        var name = dataParts[1];

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
        {
            throw new InvalidInputException("Both ID and name are required for an airline.");
        }

        if (_airlineManager.Airlines.Any(airline => airline.Id == id))
        {
            throw new DuplicateIdException("An airline with the same ID already exists.");
        }

        if (id.Length is < 2 or > 4)
        {
            throw new InvalidIdLengthException("Airline ID must be between 2 and 4 characters.");
        }

        if (!ContainsOnlyLettersOrDigits(id))
        {
            throw new InvalidIdCharactersException("Airline ID can only contain letters and digits.");
        }

        if (!ContainsOnlyLettersAndSpaces(name))
        {
            throw new InvalidAirlineNameException("Airline name can only contain letters and spaces.");
        }
    }

    public void ValidateFlightData(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new InvalidInputException("Flight data cannot be empty.");
        }

        var dataParts = data.Split(", ").ToArray();
        var id = dataParts[0];
        var departureAirportId = dataParts[1];
        var arrivalAirportId = dataParts[2];

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(departureAirportId) || string.IsNullOrEmpty(arrivalAirportId))
        {
            throw new InvalidInputException("Flight data cannot be empty.");
        }

        if (_flightManager.Flights.Any(x => x.Id == id))
        {
            throw new DuplicateIdException("A flight with the same ID already exists.");
        }

        if (departureAirportId.Length is < 2 or > 4)
        {
            throw new InvalidIdLengthException("Departure airport ID length must be between 2 and 4 characters.");
        }

        if (arrivalAirportId.Length is < 2 or > 4)
        {
            throw new InvalidIdLengthException("Arrival airport ID length must be between 2 and 4 characters.");
        }

        if (!ContainsOnlyLettersOrDigits(id))
        {
            throw new InvalidIdCharactersException("Flight ID contains invalid characters. Only letters and digits are allowed.");
        }

        if (!ContainsOnlyLettersOrDigits(departureAirportId) || !ContainsOnlyLettersOrDigits(arrivalAirportId))
        {
            throw new InvalidIdCharactersException("Airport ID contains invalid characters. Only letters and digits are allowed.");
        }
    }

    public void ValidateAircraftData(string data)
    {
        var dataParts = data.Split(", ").ToArray();
        var name = dataParts[0];

        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidInputException("Aircraft name cannot be empty.");
        }
    }

    public void ValidateRouteData(string data)
    {
        var flightId = data;

        if (string.IsNullOrEmpty(flightId))
        {
            throw new FlightNotFoundException("Flight does not exist.");
        }
    }

    private bool ContainsOnlyLettersAndSpaces(string value)
    {
        foreach (var c in value)
            if (!char.IsLetter(c) && c != ' ')
                return false;
        return true;
    }

    private bool ContainsOnlyLettersOrDigits(string value)
    {

        foreach (var c in value)
            if (!char.IsLetterOrDigit(c))
                return false;

        return true;
    }
}