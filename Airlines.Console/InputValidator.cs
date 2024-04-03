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
        if (string.IsNullOrEmpty(data))
        {
            throw new InvalidInputException("Airport data cannot be empty.");
        }

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

    public void ValidateCommandInputData(string data)
    {
        var commandParts = data.Split(' ', 2).ToArray();
        var action = commandParts[0];
        var validCommands = new Dictionary<string, HashSet<string>>
        {
            { "search", [] },
            { "sort", ["airports", "airlines", "flights"] },
            { "exist", [] },
            { "list", [] },
            { "route", ["new", "add", "remove", "print", "find"] },
            { "reserve", ["cargo", "ticket"] },
            { "batch", ["start", "run", "cancel"] }
        };

        if (!validCommands.ContainsKey(action) || commandParts.Length < 2)
        {
            throw new InvalidCommandException("The entered command is invalid.");
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
                    throw new InvalidCommandArgumentException("Invalid sort option. Please use 'airlines', 'flights', or 'airports'.");
                }
                break;

            case "exist":
                break;

            case "list":
                if (commandParts[1].Split(" ").ToArray().Length < 2)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments. Please provide exactly two arguments for this command.");
                }

                commandArguments = StringHelper.SplitBeforeLastElement(commandParts[1]);
                if (commandArguments.Length != 2)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments. Please provide exactly two arguments for this command.");
                }
                break;

            case "route":
                if (!validCommands["route"].Contains(firstArgument))
                {
                    throw new InvalidCommandArgumentException("Invalid route command. Please use one of the following commands: 'new', 'add', 'remove', 'print'.");
                }

                if (firstArgument == "add")
                {
                    var flightId = commandArguments.ElementAtOrDefault(1);
                    var flightToAdd = _flightManager.Flights.FirstOrDefault(x => x.Id == flightId);

                    ValidateRouteFlight(flightToAdd!);

                    break;
                }
                else if (firstArgument == "remove")
                {
                    if (commandArguments.Length != 2)
                    {
                        throw new InvalidNumberOfArgumentsException("Incorrect command format. Please use the following format: route remove <Departure Airport ID>");
                    }

                    if (_routeManager.Routes.Count == 0)
                    {
                        throw new EmptyRouteException("No flights to remove.");
                    }

                    if (_routeManager.Routes.ContainsKey(commandArguments[2]))
                    {
                        throw new KeyNotFoundException("Nothing to remove. The airport does not exist!");
                    }
                }
                else if (firstArgument == "print")
                {
                    if (commandArguments.Length != 2)
                    {
                        throw new InvalidNumberOfArgumentsException("Incorrect command format. Please use the following format: route print <Departure Airport ID>");
                    }
                }
                else if (firstArgument == "find")
                {
                    if (commandArguments.Length < 3)
                    {
                        throw new InvalidNumberOfArgumentsException("Incorrect command format. Please use the following format: route find <Departure Airport ID> <Arrival Airport>");
                    }

                    if (commandArguments[1] == commandArguments[2])
                    {
                        throw new InvalidCommandArgumentException("Deparute airport cannot be the same as the Arrival airport!");
                    }

                    if (!_routeManager.Routes.ContainsKey(commandArguments[1]))
                    {
                        throw new InvalidCommandArgumentException("Deparute airport does not exist!");

                    }
                }

                break;

            case "reserve":
                if (!validCommands["reserve"].Contains(firstArgument))
                {
                    throw new InvalidNumberOfArgumentsException("Invalid reserve command. Please use 'ticket' or 'cargo'.");

                }
                if (firstArgument == "cargo")
                {
                    if (commandArguments.Length != 4)
                    {
                        throw new InvalidNumberOfArgumentsException("Invalid reserve command. The arguments must be exactly 3 (flight Id, cargo weight, cargo volume).");
                    }

                    var flightId = commandArguments[1];
                    var cargoWeight = double.Parse(commandArguments[2]);
                    var cargoVolume = double.Parse(commandArguments[3]);

                    var cargoReservation = new CargoReservation(flightId, cargoWeight, cargoVolume);
                    var aircraftModel = _flightManager.GetAircraftModel(flightId);
                    var aircraft = _aircraftManager.GetCargoAircraft(aircraftModel);

                    ValidateCargoReservation(cargoReservation, aircraft!);
                }
                else if (firstArgument == "ticket")
                {
                    if (commandArguments.Length != 5)
                    {
                        throw new InvalidNumberOfArgumentsException("Invalid reserve command. The arguments must be exactly 4 (flight Id, seats, small baggage count, large baggage count).");
                    }

                    var flightId = commandArguments[1];
                    var seats = int.Parse(commandArguments[2]);
                    var smallBaggageCount = int.Parse(commandArguments[3]);
                    var largeBaggageCount = int.Parse(commandArguments[4]);

                    var ticketReservation = new TicketReservation(flightId, seats, smallBaggageCount, largeBaggageCount);

                    var aircraftModel = _flightManager.GetAircraftModel(flightId);
                    var aircraft = _aircraftManager.GetPassengerAircraft(aircraftModel);

                    ValidateTicketReservation(ticketReservation, aircraft!);
                }
                break;

            case "batch":
                if (!validCommands["batch"].Contains(firstArgument))
                {
                    throw new InvalidCommandArgumentException($"Invalid command argument. Batch command must be 'start', 'run', or 'cancel'.");
                }

                if (commandArguments.Length != 1)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments. Please provide exactly two arguments for this batch command.");
                }
                break;

            default:
                throw new InvalidCommandException($"Invalid command: {action}");
        }
    }

    private void ValidateCargoReservation(CargoReservation reservation, CargoAircraft aircraft)
    {
        if (aircraft == null)
        {
            throw new AircraftNotFoundException("No cargo aircraft available");
        }
        if (reservation == null)
        {
            throw new InvalidCargoReservationException("Cargo reservation is null.");
        }
        if (reservation.CargoWeight > aircraft.CargoWeight)
        {
            throw new InvalidCargoReservationException("Cargo weight exceeds aircraft cargo capacity.");
        }
        if (reservation.CargoVolume > aircraft.CargoVolume)
        {
            throw new InvalidCargoReservationException("Cargo volume exceeds aircraft cargo capacity.");
        }
    }
    private void ValidateTicketReservation(TicketReservation reservation, PassengerAircraft aircraft)
    {
        if (aircraft == null)
        {
            throw new AircraftNotFoundException("No passanger aircraft available");
        }
        if (reservation == null)
        {
            throw new InvalidTicketReservationException("Ticket reservation cannot be null.");
        }
        if (reservation.Seats > aircraft.Seats)
        {
            throw new InvalidTicketReservationException("Number of seats exceeds the aircraft's capacity.");
        }
        if ((reservation.SmallBaggageCount * SmallBaggageMaximumWeight) + (reservation.LargeBaggageCount * LargeBaggageMaximumWeight) > aircraft.CargoWeight)
        {
            throw new InvalidTicketReservationException("Baggage weight exceeds the aircraft's cargo capacity.");
        }

        if ((reservation.SmallBaggageCount * SmallBaggageMaximumVolume) + (reservation.LargeBaggageCount * LargeBaggageMaximumVolume) > aircraft.CargoVolume)
        {
            throw new InvalidTicketReservationException("Baggage volume exceeds the aircraft's cargo volume capacity.");
        }
    }

    private void ValidateRouteFlight(Flight flight)
    {
        if (flight == null)
        {
            throw new FlightNotFoundException("Flight does not exist.");
        }

        if (_routeManager.Routes.Count > 0)
        {
            throw new InvalidRouteException("The Route is not empty");
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