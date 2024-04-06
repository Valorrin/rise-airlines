using Airlines.Business.Commands.BatchCommands;
using Airlines.Business.Commands.ListingCommands;
using Airlines.Business.Commands.ReserveCommands;
using Airlines.Business.Commands.RouteCommands;
using Airlines.Business.Commands.SearchCommands;
using Airlines.Business.Commands.SortCommands;
using Airlines.Business.Managers;
using Airlines.Business.Models;
using Airlines.Business.Models.Reservations;
using Airlines.Business.Utilities;
using Airlines.Business.Validation;

namespace Airlines.Business.Commands;
public class CommandClient
{
    private readonly CommandInvoker _invoker;
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly RouteManager _routeManager;
    private readonly ReservationsManager _reservationsManager;
    private readonly BatchManager _batchManager;
    private readonly CommandValidator _commandValidator;

    public CommandClient(CommandInvoker invoker,
                             AirportManager airportManager,
                             AirlineManager airlineManager,
                             FlightManager flightManager,
                             RouteManager routeManager,
                             ReservationsManager reservationsManager,
                             BatchManager batchManager,
                             CommandValidator commandValidator)
    {
        _invoker = invoker;
        _airportManager = airportManager;
        _airlineManager = airlineManager;
        _flightManager = flightManager;
        _routeManager = routeManager;
        _reservationsManager = reservationsManager;
        _batchManager = batchManager;
        _commandValidator = commandValidator;
    }

    public void ProcessCommand(string command, bool batchMode)
    {
        var commandParts = command.Split(' ', 2).ToArray();
        var action = commandParts[0];
        var commandArguments = commandParts[1].Split().ToArray();

        if (action == "search")
        {
            var searchTerm = commandParts[1];

            _commandValidator.ValidateSearchCommand(searchTerm);
            ProcessSearchCommand(searchTerm, batchMode);
        }
        else if (action == "sort")
        {
            var target = commandArguments[0];
            var sortOrder = commandArguments.ElementAtOrDefault(1);

            _commandValidator.ValidateSortCommand(target, sortOrder!);
            ProcessSortCommand(target, sortOrder!, batchMode);
        }
        else if (action == "exist")
        {
            var airportName = commandParts[1];

            _commandValidator.ValidateExistCommand(airportName);
            ProcessExistCommand(airportName, batchMode);

        }
        else if (action == "list")
        {
            commandParts = StringHelper.SplitBeforeLastElement(commandParts[1]);
            var inputData = commandParts[0];
            var from = commandParts[1];

            _commandValidator.ValidateListCommand(inputData, from);
            ProcessListCommand(inputData, from, batchMode);
        }
        else if (action == "route")
        {

            var commandAction = commandArguments[0];

            Flight flightToAdd = null!;

            Airport startAirport = null!;
            Airport endAirport = null!;

            if (commandAction is "check" or "search")
            {
                var startAirportId = commandArguments[1];
                var endAirportId = commandArguments[2];

                startAirport = _airportManager.GetAirportById(startAirportId);
                endAirport = _airportManager.GetAirportById(endAirportId);
            }

            _commandValidator.ValidateRouteCommand(commandAction, flightToAdd, startAirport, endAirport);
            ProcessRouteCommand(commandAction, flightToAdd!, startAirport!, endAirport!, batchMode);
        }
        else if (action == "reserve")
        {


            ProcessReserveCommand(commandArguments, batchMode);
        }
        else if (action == "batch")
        {
            var commandAction = commandArguments[0];

            ProcessBatchCommand(commandAction);
        }
    }

    private void ProcessSearchCommand(string searchTerm, bool batchMode)
    {
        var searchCommand = SearchCommand.CreateSearchCommand(_airportManager, _airlineManager, _flightManager, searchTerm);

        if (batchMode)
            _batchManager.AddCommand(searchCommand);
        else
            _invoker.ExecuteCommand(searchCommand);
    }

    private void ProcessSortCommand(string target, string sortOrder, bool batchMode)
    {

        if (target == "airports")
        {
            var sortAirportsCommand = SortAirportsCommand.CreateSortAirportsCommand(_airportManager, sortOrder!);

            if (batchMode)
            {
                _batchManager.AddCommand(sortAirportsCommand);
            }
            else
            {
                _invoker.ExecuteCommand(sortAirportsCommand);
            }
        }
        else if (target == "airlines")
        {
            var sortAirlinesCommand = SortAirlinesCommand.CreateSortAirlinesCommand(_airlineManager, sortOrder!);

            if (batchMode)
            {
                _batchManager.AddCommand(sortAirlinesCommand);
            }
            else
            {
                _invoker.ExecuteCommand(sortAirlinesCommand);
            }
        }
        else if (target == "flights")
        {
            var sortFlightsCommand = SortFlightsCommand.CreateSortFlightsCommand(_flightManager, sortOrder!);

            if (batchMode)
            {
                _batchManager.AddCommand(sortFlightsCommand);
            }
            else
            {
                _invoker.ExecuteCommand(sortFlightsCommand);
            }
        }
    }

    private void ProcessExistCommand(string airportName, bool batchMode)
    {
        var existCommand = CheckAirportExistenceCommand.CreateCheckAirportExistenceCommand(_airportManager, airportName);

        if (batchMode)
            _batchManager.AddCommand(existCommand);
        else
            _invoker.ExecuteCommand(existCommand);
    }

    private void ProcessListCommand(string inputData, string from, bool batchMode)
    {
        var listCommand = ListDataCommand.CreateListDataCommand(_airportManager, inputData, from);

        if (batchMode)
            _batchManager.AddCommand(listCommand);
        else
            _invoker.ExecuteCommand(listCommand);
    }

    private void ProcessRouteCommand(string commandAction, Flight flightToAdd, Airport startAirport, Airport endAirport, bool batchMode)
    {

        ICommand? routeCommand = null;

        switch (commandAction)
        {
            case "new":
                routeCommand = RouteNewCommand.CreateRouteNewCommand(_routeManager);
                break;

            case "add":
                routeCommand = RouteAddCommand.CreateRouteAddCommand(_routeManager, flightToAdd);
                break;

            case "remove":
                routeCommand = RouteRemoveCommand.CreateRouteRemoveCommand(_routeManager);
                break;

            case "print":
                routeCommand = RoutePrintCommand.CreateRoutePrintCommand(_routeManager);
                break;

            case "find":
                routeCommand = RouteFindCommand.CreateRouteFindCommand(_routeManager, endAirport);
                break;

            case "check":
                routeCommand = RouteCheckCommand.CreateRouteCheckCommand(_routeManager, startAirport, endAirport);
                break;

            case "search":
                routeCommand = RouteSearchCommand.CreateRouteSearchCommand(_routeManager, startAirport, endAirport);
                break;

            default:
                break;
        }

        if (routeCommand != null)
        {
            if (batchMode)
                _batchManager.AddCommand(routeCommand);
            else
                _invoker.ExecuteCommand(routeCommand);
        }
    }

    private void ProcessReserveCommand(string[] commandArguments, bool batchMode)
    {

        var commandAction = commandArguments[0];
        var flightId = commandArguments[1];

        ICommand? reservationCommand = null;

        switch (commandAction)
        {
            case "cargo":
                if (commandArguments.Length >= 4)
                {
                    var cargoWeight = double.Parse(commandArguments[2]);
                    var cargoVolume = double.Parse(commandArguments[3]);

                    var cargoReservation = new CargoReservation(flightId, cargoWeight, cargoVolume);

                    reservationCommand = ReserveCargoCommand.CreateReserveCargoCommand(_reservationsManager, cargoReservation);
                }
                break;
            case "ticket":
                if (commandArguments.Length >= 5)
                {
                    var seats = int.Parse(commandArguments[2]);
                    var smallBaggageCount = int.Parse(commandArguments[3]);
                    var largeBaggageCount = int.Parse(commandArguments[4]);

                    var ticketReservation = new TicketReservation(flightId, seats, smallBaggageCount, largeBaggageCount);

                    reservationCommand = ReserveTicketCommand.CreateTicketCommand(_reservationsManager, ticketReservation);
                }
                break;
            default:
                break;
        }

        if (reservationCommand != null)
        {
            if (batchMode)
                _batchManager.AddCommand(reservationCommand);
            else
                _invoker.ExecuteCommand(reservationCommand);
        }
        else
        {
            Console.WriteLine("Error: Invalid reservation command or parameters.");
        }
    }

    private void ProcessBatchCommand(string commandAction)
    {

        ICommand? batchCommand = null;

        switch (commandAction)
        {
            case "start":
                batchCommand = BatchStartCommand.CreateBatchStartCommand(_batchManager);
                break;
            case "run":
                batchCommand = BatchRunCommand.CreateBatchRunCommand(_batchManager, _invoker);
                break;
            case "cancel":
                batchCommand = BatchCancelCommand.CreateBatchCancelCommand(_batchManager);
                break;
            default:
                break;
        }

        if (batchCommand != null)
        {
            _invoker.ExecuteCommand(batchCommand);
        }
        else
        {
            Console.WriteLine("Error: Invalid batch command.");
        }
    }
}