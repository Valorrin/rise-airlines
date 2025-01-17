﻿using Airlines.Business.Commands.BatchCommands;
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
    private readonly ReservationManager _reservationsManager;
    private readonly BatchManager _batchManager;
    private readonly CommandValidator _commandValidator;

    public CommandClient(CommandInvoker invoker,
                             AirportManager airportManager,
                             AirlineManager airlineManager,
                             FlightManager flightManager,
                             RouteManager routeManager,
                             ReservationManager reservationsManager,
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

    public void ProcessCommand(string command, bool isBatchMode)
    {
        var commandParts = command.Split(' ', 2).ToArray();
        var action = commandParts[0];
        var commandArguments = commandParts[1].Split().ToArray();

        if (action == "search")
        {
            var searchTerm = commandParts[1];

            _commandValidator.ValidateSearchCommand(searchTerm);
            ProcessSearchCommand(searchTerm, isBatchMode);
        }
        else if (action == "sort")
        {
            var target = commandArguments[0];
            var sortOrder = commandArguments.ElementAtOrDefault(1);

            _commandValidator.ValidateSortCommand(target, sortOrder!);
            ProcessSortCommand(target, sortOrder!, isBatchMode);
        }
        else if (action == "exist")
        {
            var airportName = commandParts[1];

            _commandValidator.ValidateExistCommand(airportName);
            ProcessExistCommand(airportName, isBatchMode);

        }
        else if (action == "list")
        {
            commandParts = StringHelper.SplitBeforeLastElement(commandParts[1]);
            var inputData = commandParts[0];
            var from = commandParts[1];

            _commandValidator.ValidateListCommand(inputData, from);
            ProcessListCommand(inputData, from, isBatchMode);
        }
        else if (action == "route")
        {

            var commandAction = commandArguments[0];

            Flight flightToAdd = null!;
            Airport startAirport = null!;
            Airport endAirport = null!;
            string strategy = null!;

            if (commandAction is "check" or "search")
            {
                var startAirportId = commandArguments[1];
                var endAirportId = commandArguments[2];

                startAirport = _airportManager.GetAirportById(startAirportId);
                endAirport = _airportManager.GetAirportById(endAirportId);
            }
            if (commandAction is "add")
            {
                flightToAdd = _flightManager.GetFlightById(commandArguments[1]);
            }
            if (commandAction is "search")
            {
                strategy = commandArguments[3];
            }
            if (commandAction is "find")
            {
                var endAirportId = commandArguments[1];
                endAirport = _airportManager.GetAirportById(endAirportId);
            }

            _commandValidator.ValidateRouteCommand(commandAction, flightToAdd, startAirport, endAirport, strategy);
            ProcessRouteCommand(commandAction, flightToAdd!, startAirport!, endAirport!, strategy!, isBatchMode);
        }
        else if (action == "reserve")
        {

            _commandValidator.ValidateReserveCommand(commandArguments);
            ProcessReserveCommand(commandArguments, isBatchMode);
        }
        else if (action == "batch")
        {
            var commandAction = commandArguments[0];
            ProcessBatchCommand(commandAction);
        }
    }

    private void ProcessSearchCommand(string searchTerm, bool isBatchMode)
    {
        var searchCommand = new SearchCommand(_airportManager, _airlineManager, _flightManager, searchTerm);
        ProcessCommandBasedOnMode(searchCommand, isBatchMode);
    }

    private void ProcessSortCommand(string target, string sortOrder, bool isBatchMode)
    {

        if (target == "airports")
        {
            var sortAirportsCommand = new SortAirportsCommand(_airportManager, sortOrder!);
            ProcessCommandBasedOnMode(sortAirportsCommand, isBatchMode);
        }
        else if (target == "airlines")
        {
            var sortAirlinesCommand = new SortAirlinesCommand(_airlineManager, sortOrder!);
            ProcessCommandBasedOnMode(sortAirlinesCommand, isBatchMode);
        }
        else if (target == "flights")
        {
            var sortFlightsCommand = new SortFlightsCommand(_flightManager, sortOrder!);
            ProcessCommandBasedOnMode(sortFlightsCommand, isBatchMode);
        }
    }

    private void ProcessExistCommand(string airportName, bool isBatchMode)
    {
        var existCommand = new CheckAirportExistenceCommand(_airportManager, airportName);
        ProcessCommandBasedOnMode(existCommand, isBatchMode);
    }

    private void ProcessListCommand(string inputData, string from, bool isBatchMode)
    {
        var listCommand = new ListDataCommand(_airportManager, inputData, from);
        ProcessCommandBasedOnMode(listCommand, isBatchMode);
    }

    private void ProcessRouteCommand(string commandAction, Flight flightToAdd, Airport startAirport, Airport endAirport, string strategy, bool isBatchMode)
    {
        ICommand? routeCommand = null;

        switch (commandAction)
        {
            case "new":
                routeCommand = new RouteNewCommand(_routeManager);
                break;

            case "add":
                routeCommand = new RouteAddCommand(_routeManager, flightToAdd);
                break;

            case "remove":
                routeCommand = new RouteRemoveCommand(_routeManager);
                break;

            case "print":
                routeCommand = new RoutePrintCommand(_routeManager);
                break;

            case "find":
                routeCommand = new RouteFindCommand(_routeManager, endAirport);
                break;

            case "check":
                routeCommand = new RouteCheckCommand(_routeManager, startAirport, endAirport);
                break;

            case "search":
                routeCommand = new RouteSearchCommand(_routeManager, startAirport, endAirport, strategy);
                break;

            default:
                break;
        }

        if (routeCommand != null)
        {
            ProcessCommandBasedOnMode(routeCommand, isBatchMode);
        }
    }

    private void ProcessReserveCommand(string[] commandArguments, bool isBatchMode)
    {

        var commandAction = commandArguments[0];
        var flightId = commandArguments[1];
        double cargoWeight;
        double cargoVolume;
        int seats;
        int smallBaggageCount;
        int largeBaggageCount;

        ICommand? reservationCommand = null;

        switch (commandAction)
        {
            case "cargo":
                cargoWeight = double.Parse(commandArguments[2]);
                cargoVolume = double.Parse(commandArguments[3]);

                var cargoReservation = new CargoReservation(flightId, cargoWeight, cargoVolume);

                reservationCommand = new ReserveCargoCommand(_reservationsManager, cargoReservation);
                break;

            case "ticket":
                seats = int.Parse(commandArguments[2]);
                smallBaggageCount = int.Parse(commandArguments[3]);
                largeBaggageCount = int.Parse(commandArguments[4]);

                var ticketReservation = new TicketReservation(flightId, seats, smallBaggageCount, largeBaggageCount);

                reservationCommand = new ReserveTicketCommand(_reservationsManager, ticketReservation);
                break;

            default:
                break;
        }

        if (reservationCommand != null)
        {
            ProcessCommandBasedOnMode(reservationCommand, isBatchMode);
        }
        else
        {
            throw new ArgumentException("Error: Invalid reservation command or parameters.");
        }
    }

    private void ProcessBatchCommand(string commandAction)
    {

        ICommand? batchCommand = null;

        switch (commandAction)
        {
            case "start":
                batchCommand = new BatchStartCommand(_batchManager);
                break;
            case "run":
                batchCommand = new BatchRunCommand(_batchManager, _invoker);
                break;
            case "cancel":
                batchCommand = new BatchCancelCommand(_batchManager);
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
            throw new ArgumentException("Error: Invalid batch command.");
        }
    }

    private void ProcessCommandBasedOnMode(ICommand command, bool isBatchMode)
    {
        if (isBatchMode)
        {
            _batchManager.AddCommand(command);
        }
        else
        {
            _invoker.ExecuteCommand(command);
        }
    }
}