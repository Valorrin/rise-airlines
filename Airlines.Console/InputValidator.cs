using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Models.Aircrafts;
using Airlines.Business.Models.Reservations;
using Airlines.Business.Utilities;
using Airlines.Console.Exceptions;

namespace Airlines.Console;
public class InputValidator
{
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;
    private readonly RouteManager _routeManager;

    public const int SmallBaggageMaximumWeight = 15;
    public const double SmallBaggageMaximumVolume = 0.045;

    public const int LargeBaggageMaximumWeight = 30;
    public const double LargeBaggageMaximumVolume = 0.090;

    public InputValidator(AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager,
        AircraftManager aircraftManager, RouteManager routeManager)
    {
        _airportManager = airportManager;
        _airlineManager = airlineManager;
        _flightManager = flightManager;
        _aircraftManager = aircraftManager;
        _routeManager = routeManager;
    }

    public void ValidateAirportData(string data)
    {
        var dataParts = data.Split(", ").ToArray();
        var id = dataParts[0];
        var name = dataParts[1];
        var city = dataParts[2];
        var country = dataParts[3];

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
        {
            throw new InvalidInputException("Airport data cannot be empty.");
        }

        if (_airportManager.Airports.ContainsKey(id))
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
    public void ValidateAirportData(IList<string> data)
    {
        var ids = data.Select(line => line.Split(", ", 2)[0]);

        if (ids.Distinct().Count() != data.Count)
        {
            throw new DuplicateIdException("Airport IDs must be unique. Duplicate IDs are not allowed.");
        }

        foreach (var line in data)
        {
            ValidateAirportData(line);

        }
    }

    public void ValidateAirlineData(string data)
    {
        var dataParts = data.Split(", ").ToArray();
        var id = dataParts[0];
        var name = dataParts[1];

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
        {
            throw new InvalidInputException("Both ID and name are required for an airline.");
        }

        if (_airlineManager.Airlines.ContainsKey(id))
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
    public void ValidateAirlineData(IList<string> data)
    {
        var ids = data.Select(line => line.Split(", ", 2)[0]);

        if (ids.Distinct().Count() != data.Count)
        {
            throw new DuplicateIdException("Airline IDs must be unique. Duplicate IDs are not allowed.");
        }

        foreach (var line in data)
        {
            ValidateAirlineData(line);
        }
    }

    public void ValidateFlightData(string data)
    {
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
    public void ValidateFlightData(IList<string> data)
    {
        var ids = data.Select(line => line.Split(", ", 2)[0]);

        if (ids.Distinct().Count() != data.Count)
        {
            throw new DuplicateIdException("Flight IDs must be unique. Duplicate IDs are not allowed.");
        }

        foreach (var line in data)
        {
            ValidateFlightData(line);
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
    public void ValidateAircraftData(IList<string> data)
    {
        foreach (var line in data)
        {
            ValidateAircraftData(line);
        }
    }

    public bool ValidateCommandInputData(string data)
    {
        var commandParts = data.Split(' ', 2).ToArray();
        var action = commandParts[0];
        var validCommands = new Dictionary<string, HashSet<string>>
        {
            { "search", [] },
            { "sort", ["airports", "airlines", "flights"] },
            { "exist", [] },
            { "list", [] },
            { "route", ["new", "add", "remove", "print"] },
            { "reserve", ["cargo", "ticket"] },
            { "batch", ["start", "run", "cancel"] }
        };

        if (!validCommands.ContainsKey(action) || commandParts.Length < 2)
        {
            System.Console.WriteLine("Invalid command");
            return false;
        }

        var commandArguments = commandParts[1].Split().ToArray();
        var firstArgument = commandArguments[0];

        switch (action)
        {
            case "search":
                break;

            case "sort":
                if (!validCommands["sort"].Contains(firstArgument))
                    return false;
                break;

            case "exist":
                break;

            case "list":
                commandArguments = StringHelper.SplitBeforeLastElement(commandParts[1]);
                if (commandArguments.Length != 2)
                    return false;
                break;

            case "route":
                if (!validCommands["route"].Contains(firstArgument))
                    return false;

                if (firstArgument == "add")
                {
                    var flightId = commandArguments.ElementAtOrDefault(1);
                    var flightToAdd = _flightManager.Flights.FirstOrDefault(x => x.Id == flightId);

                    if (flightToAdd == null || !ValidateRouteFlight(flightToAdd))
                    {
                        System.Console.WriteLine($" Error: Flight does not exist.");
                        return false;
                    }
                }
                else if (firstArgument == "remove")
                    if (_routeManager.Routes.Count == 0)
                    {
                        System.Console.WriteLine("Error: No flights in route.");
                        return false;
                    }
                break;

            case "reserve":
                if (!validCommands["reserve"].Contains(firstArgument))
                    return false;
                if (firstArgument == "cargo")
                {
                    var flightId = commandArguments[1];
                    var cargoWeight = double.Parse(commandArguments[2]);
                    var cargoVolume = double.Parse(commandArguments[3]);

                    var cargoReservation = new CargoReservation(flightId, cargoWeight, cargoVolume);
                    var aircraftModel = _flightManager.GetAircraftModel(flightId);
                    var aircraft = _aircraftManager.GetCargoAircraft(aircraftModel);

                    if (aircraft == null || !ValidateCargoReservation(cargoReservation, aircraft))
                        return false;
                }
                else if (firstArgument == "ticket")
                {
                    var flightId = commandArguments[1];
                    var seats = int.Parse(commandArguments[2]);
                    var smallBaggageCount = int.Parse(commandArguments[3]);
                    var largeBaggageCount = int.Parse(commandArguments[4]);

                    var ticketReservation = new TicketReservation(flightId, seats, smallBaggageCount, largeBaggageCount);

                    var aircraftModel = _flightManager.GetAircraftModel(flightId);
                    var aircraft = _aircraftManager.GetPassengerAircraft(aircraftModel);

                    if (aircraft == null || !ValidateTicketReservation(ticketReservation, aircraft))
                        return false;
                }
                break;

            case "batch":
                if (!validCommands["batch"].Contains(firstArgument) && commandArguments.Length != 2)
                    return false;
                break;

            default:
                System.Console.WriteLine($"Invalid command: {action}");
                return false;
        }

        return true;
    }

    private bool ValidateCargoReservation(CargoReservation reservation, CargoAircraft aircraft)
    {
        if (reservation == null || aircraft == null)
        {
            System.Console.WriteLine("Reservation or aircraft is null");
            return false;
        }
        if (reservation.CargoWeight > aircraft.CargoWeight)
        {
            System.Console.WriteLine("cargo weight exceeds aircraft cargo capacity");
            return false;
        }
        if (reservation.CargoVolume > aircraft.CargoVolume)
        {
            System.Console.WriteLine("cargo volume exceeds aircraft cargo capacity");
            return false;
        }

        System.Console.WriteLine("Cargo validataion aproved");
        return true;
    }
    private bool ValidateTicketReservation(TicketReservation reservation, PassengerAircraft aircraft)
    {
        if (reservation == null || aircraft == null)
        {
            System.Console.WriteLine("Reservation or aircraft is null");
            return false;
        }
        if (reservation.Seats > aircraft.Seats)
        {
            System.Console.WriteLine("not enough seats");
            return false;
        }
        if ((reservation.SmallBaggageCount * SmallBaggageMaximumWeight) + (reservation.LargeBaggageCount * LargeBaggageMaximumWeight) > aircraft.CargoWeight)
        {
            System.Console.WriteLine("cargo weight exceeds aircraft cargo capacity");
            return false;
        }

        if ((reservation.SmallBaggageCount * SmallBaggageMaximumVolume) + (reservation.LargeBaggageCount * LargeBaggageMaximumVolume) > aircraft.CargoVolume)
        {
            System.Console.WriteLine("cargo volume exceeds aircraft cargo capacity");
            return false;
        }

        return true;
    }

    private bool ValidateRouteFlight(Flight flight)
    {
        if (_routeManager.Routes.Count == 0)
            return true;

        if (_routeManager.Routes.Last!.Value.ArrivalAirport == flight.DepartureAirport)
            return true;

        System.Console.WriteLine(" ERROR: The DepartureAirport of the new flight doesn't matches the ArrivalAirport of the last flight in the route!");
        return false;
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