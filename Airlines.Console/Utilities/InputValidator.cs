using Airlines.Business.Managers;
using Airlines.Business.Models.Aircrafts;
using Airlines.Business.Models.Reservations;
using Airlines.Business.Utilities;

namespace Airlines.Console.Utilities;
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
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }
        if (string.IsNullOrEmpty(city))
        {
            return false;
        }
        if (string.IsNullOrEmpty(country))
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
            if (!char.IsLetter(c) && c != ' ')
            {
                return false;
            }
        }

        foreach (var c in city)
        {
            if (!char.IsLetter(c) && c != ' ')
            {
                return false;
            }
        }

        foreach (var c in country)
        {
            if (!char.IsLetter(c) && c != ' ')
            {
                return false;
            }
        }

        return true;
    }
    public bool ValidateAirportData(IList<string> data)
    {

        foreach (var line in data)
        {
            var isValid = ValidateAirportData(line);

            if (!isValid)
            {
                return false;
            }
        }

        return true;
    }

    public bool ValidateAirlineData(string data)
    {
        var dataParts = data.Split(", ").ToArray();
        var id = dataParts[0];
        var name = dataParts[1];

        if (string.IsNullOrEmpty(id))
        {
            return false;
        }
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        if (_airlineManager.Airlines.ContainsKey(id))
        {
            System.Console.WriteLine(" Error: An airline with the same ID already exists.");
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
            if (!char.IsLetter(c) && c != ' ')
            {
                return false;
            }
        }

        return true;
    }
    public bool ValidateAirlineData(IList<string> data)
    {

        foreach (var line in data)
        {
            var isValid = ValidateAirlineData(line);

            if (!isValid)
            {
                return false;
            }
        }

        return true;
    }

    public bool ValidateFlightData(string data)
    {
        var dataParts = data.Split(", ").ToArray();
        var id = dataParts[0];
        var departureAirportId = dataParts[1];
        var arrivalAirportId = dataParts[2];

        if (string.IsNullOrEmpty(id))
        {
            return false;
        }
        if (string.IsNullOrEmpty(departureAirportId))
        {
            return false;
        }
        if (string.IsNullOrEmpty(arrivalAirportId))
        {
            return false;
        }

        if (_flightManager.Flights.Any(x => x.Id == id))
        {
            System.Console.WriteLine(" Error: A flight with the same ID already exists.");
            return false;
        }

        if (departureAirportId.Length is < 2 or > 4)
        {
            return false;
        }
        if (arrivalAirportId.Length is < 2 or > 4)
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
        foreach (var c in departureAirportId)
        {
            if (!char.IsLetterOrDigit(c))
            {
                return false;
            }
        }
        foreach (var c in arrivalAirportId)
        {
            if (!char.IsLetterOrDigit(c))
            {
                return false;
            }
        }

        return true;
    }
    public bool ValidateFlightData(IList<string> data)
    {

        foreach (var line in data)
        {
            var isValid = ValidateFlightData(line);

            if (!isValid)
            {
                return false;
            }
        }

        return true;
    }

    public bool ValidateAircraftData(string data)
    {
        var dataParts = data.Split(", ").ToArray();
        var name = dataParts[0];

        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        return true;
    }
    public bool ValidateAircraftData(IList<string> data)
    {

        foreach (var line in data)
        {
            var isValid = ValidateAircraftData(line);

            if (!isValid)
            {
                return false;
            }
        }

        return true;
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
                {
                    return false;
                }
                break;

            case "exist":
                break;

            case "list":
                commandArguments = StringHelper.SplitBeforeLastElement(commandParts[1]);
                if (commandArguments.Length != 2)
                {
                    return false;
                }
                break;

            case "route":
                if (!validCommands["route"].Contains(firstArgument))
                {
                    return false;
                }
                if (firstArgument == "remove")
                {
                    if (_routeManager.IsEmpty())
                    {
                        System.Console.WriteLine("Error: No flights in route.");
                        return false;
                    }
                }
                break;

            case "reserve":
                if (!validCommands["reserve"].Contains(firstArgument))
                {
                    return false;
                }
                if (firstArgument == "cargo")
                {
                    var flightId = commandArguments[1];
                    var cargoWeight = double.Parse(commandArguments[2]);
                    var cargoVolume = double.Parse(commandArguments[3]);

                    var cargoReservation = new CargoReservation(flightId, cargoWeight, cargoVolume);
                    var aircraftModel = _flightManager.GetAircraftModel(flightId);
                    var aircraft = _aircraftManager.GetCargoAircraft(aircraftModel);

                    if (aircraft == null || !ValidateCargoReservation(cargoReservation, aircraft))
                    {
                        return false;
                    }
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
                    {
                        return false;
                    }
                }
                break;

            case "batch":
                if (!validCommands["batch"].Contains(firstArgument) && commandArguments.Length != 2)
                {
                    return false;
                }
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
        if (((reservation.SmallBaggageCount * SmallBaggageMaximumWeight) + (reservation.LargeBaggageCount * LargeBaggageMaximumWeight)) > aircraft.CargoWeight)
        {
            System.Console.WriteLine("cargo weight exceeds aircraft cargo capacity");
            return false;
        }

        if (((reservation.SmallBaggageCount * SmallBaggageMaximumVolume) + (reservation.LargeBaggageCount * LargeBaggageMaximumVolume)) > aircraft.CargoVolume)
        {
            System.Console.WriteLine("cargo volume exceeds aircraft cargo capacity");
            return false;
        }

        return true;
    }
}