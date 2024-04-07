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

    public void ValidateCommandArguments(string commandInput)
    {
        var commandParts = commandInput.Split(' ', 2).ToArray();
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
            var validCommandsList = string.Join(", ", validCommands.Keys);

            throw new InvalidCommandException($"The entered command is invalid. Valid commands are: {validCommandsList}.");
        }

        var commandArguments = commandParts[1].Split().ToArray();
        var argumentsCount = commandArguments.Length;


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
                if (commandArguments.Length == 1)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'list' command. Expected 2 arguments.");
                }

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
                if (routeAction is "check" && argumentsCount != 3)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for'route check' command. Expected 3 arguments.");
                }
                if (routeAction is "search" && argumentsCount != 3)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'route search' command. Expected 3 arguments.");
                }
                break;
            case "reserve":
                var reserveAction = commandArguments[0];
                if (reserveAction == "cargo" && argumentsCount != 4)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'reserve cargo' command. Expected 4 arguments.");
                }
                if (reserveAction == "ticket" && argumentsCount != 5)
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments for 'reserve ticket' command. Expected 5 arguments.");
                }
                break;
            case "batch":
                if (commandArguments.Length != 1 && commandArguments[0] is not "new" or "run" or "cancel")
                {
                    throw new InvalidNumberOfArgumentsException("Invalid number of arguments. Please provide exactly two arguments for this batch command.");
                }
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

    public void ValidateReserveCommand(string[] commandArguments)
    {
        var commandAction = commandArguments[0];
        var flightId = commandArguments[1];

        if (commandAction == "cargo")
        {
            var cargoWeight = double.Parse(commandArguments[2]);
            var cargoVolume = double.Parse(commandArguments[3]);

            var cargoReservation = new CargoReservation(flightId, cargoWeight, cargoVolume);
            var aircraftModel = _flightManager.GetAircraftModel(flightId);
            var aircraft = _aircraftManager.GetCargoAircraft(aircraftModel);

            ValidateCargoReservation(cargoReservation, aircraft!);
        }

        if (commandAction == "ticket")
        {
            var seats = int.Parse(commandArguments[2]);
            var smallBaggageCount = int.Parse(commandArguments[3]);
            var largeBaggageCount = int.Parse(commandArguments[4]);

            var ticketReservation = new TicketReservation(flightId, seats, smallBaggageCount, largeBaggageCount);

            var aircraftModel = _flightManager.GetAircraftModel(flightId);
            var aircraft = _aircraftManager.GetPassengerAircraft(aircraftModel);

            ValidateTicketReservation(ticketReservation, aircraft!);
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
}
