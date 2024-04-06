using Airlines.Business.Exceptions;
using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Models.Aircrafts;
using Airlines.Business.Models.Reservations;
using Airlines.Business.Utilities;


namespace Airlines.Business.Validation;
public class CommandValidator
{
    private readonly AirportManager _airportManager;
    private readonly FlightManager _flightManager;
    private readonly AircraftManager _aircraftManager;
    private readonly RouteManager _routeManager;

    public const int SmallBaggageMaximumWeight = 15;
    public const double SmallBaggageMaximumVolume = 0.045;

    public const int LargeBaggageMaximumWeight = 30;
    public const double LargeBaggageMaximumVolume = 0.090;

    public CommandValidator(AirportManager airportManager, FlightManager flightManager,
        AircraftManager aircraftManager, RouteManager routeManager)
    {
        _airportManager = airportManager;
        _flightManager = flightManager;
        _aircraftManager = aircraftManager;
        _routeManager = routeManager;
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
            { "route", ["new", "add", "remove", "print", "find", "check", "search"] },
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

                    break;
                }
                else if (firstArgument == "remove")
                {

                    if (_routeManager.Route.AdjacencyList.Values.Count == 0)
                    {
                        throw new EmptyRouteException("No flights to remove.");
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



    public void ValidateCommandArguments(string commandInput)
    {
        var commandParts = commandInput.Split(' ', 2).ToArray();
        var action = commandParts[0];
        var commandArguments = commandParts[1].Split().ToArray();
        var argumentsCount = commandArguments.Length;

        var validCommands = new Dictionary<string, HashSet<string>>
        {
            { "search", [] },
            { "sort", ["airports", "airlines", "flights"] },
            { "exist", [] },
            { "list", [] },
            { "route", ["new", "add", "remove", "print", "find", "check", "search"] },
            { "reserve", ["cargo", "ticket"] },
            { "batch", ["start", "run", "cancel"] }
        };

        if (!validCommands.ContainsKey(action) || commandParts.Length < 2)
        {
            var validCommandsList = string.Join(", ", validCommands.Keys);

            throw new InvalidCommandException($"The entered command is invalid. Valid commands are: {validCommandsList}.");
        }


        switch (action)
        {
            case "search":
                if (argumentsCount != 1)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'search' command. Expected 1 argument.");
                }
                break;
            case "sort":
                if (argumentsCount != 2)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'sort' command. Expected 2 arguments.");
                }
                break;
            case "exist":
                if (argumentsCount != 1)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'exist' command. Expected 1 argument.");
                }
                break;
            case "list":
                var listArguments = StringHelper.SplitBeforeLastElement(commandParts[1]);
                if (listArguments.Length != 2)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'list' command. Expected 2 arguments.");
                }
                break;
            case "route":
                var routeAction = commandArguments[0];
                if (routeAction == "add" && argumentsCount != 2)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'route add' command. Expected 2 arguments.");
                }
                if (routeAction is "add" or "check" or "search" && argumentsCount != 3)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'route add', 'route check', or 'route search' command. Expected 3 arguments.");
                }
                break;
            case "reserve":
                //TODO
                break;
            case "batch":
                //TODO
                break;
            default:
                break;
        }
    }

    public void ValidateSearchCommand(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            throw new InvalidCommandArgumentException("Search term cannot be empty.");
        }
    }

    public void ValidateSortCommand(string target, string sortOrder = "")
    {
        var sortOption = target + sortOrder;

        if (!sortOption.Equals("airports", StringComparison.OrdinalIgnoreCase) &&
            !sortOption.Equals("airlines", StringComparison.OrdinalIgnoreCase) &&
            !sortOption.Equals("flights", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCommandArgumentException("Invalid sort option. Please use 'airlines', 'flights', or 'airports'.");
        }
    }

    public void ValidateExistCommand(string airportName)
    {
        if (string.IsNullOrEmpty(airportName))
        {
            throw new InvalidCommandArgumentException("Airport name cannot be null or empty.");
        }
    }

    public void ValidateListCommand(string inputData, string from)
    {
        if (string.IsNullOrEmpty(inputData))
        {
            throw new InvalidCommandArgumentException("Airport name cannot be null or empty.");
        }

        if (from is not "Country" and not "City")
        {
            throw new InvalidCommandArgumentException("Invalid 'from' parameter. Please use 'Country' or 'City'.");
        }
    }

    public void ValidateRouteCommand(string commandAction, Flight flightToAdd, Airport startAirport, Airport endAirport)
    {
        if (commandAction == "add")
        {
            if (flightToAdd == null)
            {
                throw new InvalidCommandArgumentException("");
            }
        }

        if (commandAction == "remove")
        {

            if (_routeManager.Route.AdjacencyList.Values.Count == 0)
            {
                throw new EmptyRouteException("No flights to remove.");
            }
        }

        if (commandAction is "check" or "search")
        {
            if (startAirport == null)
            {
                throw new InvalidCommandArgumentException("");
            }

            if (endAirport == null)
            {
                throw new InvalidCommandArgumentException("");
            }

            if (!_airportManager.Airports.Any(x => x == startAirport))
            {
                throw new AirportNotFoundException($"Start airport '{startAirport.Name}' not found.");
            }

            if (!_airportManager.Airports.Any(x => x == endAirport))
            {
                throw new AirportNotFoundException($"End airport '{endAirport.Name}' not found.");
            }
        }
    }
}
