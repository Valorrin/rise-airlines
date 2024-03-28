﻿using Airlines.Business.Commands.ListingCommands;
using Airlines.Business.Commands.ReserveCommands;
using Airlines.Business.Commands.RouteCommands;
using Airlines.Business.Commands.SearchCommands;
using Airlines.Business.Commands.SortCommands;
using Airlines.Business.Managers;
using Airlines.Business.Models.Reservations;
using Airlines.Business.Utilities;

namespace Airlines.Business.Commands;
public class CommandClient
{
    private readonly CommandInvoker _invoker;
    private readonly AirportManager _airportManager;
    private readonly AirlineManager _airlineManager;
    private readonly FlightManager _flightManager;
    private readonly RouteManager _routeManager;
    private readonly AircraftManager _aircraftManager;
    private readonly ReservationsManager _reservationsManager;

    public CommandClient(CommandInvoker invoker,
                             AirportManager airportManager,
                             AirlineManager airlineManager,
                             FlightManager flightManager,
                             RouteManager routeManager,
                             AircraftManager aircraftManager,
                             ReservationsManager reservationsManager)
    {
        _invoker = invoker;
        _airportManager = airportManager;
        _airlineManager = airlineManager;
        _flightManager = flightManager;
        _routeManager = routeManager;
        _aircraftManager = aircraftManager;
        _reservationsManager = reservationsManager;
    }

    public void ProcessCommand(string command)
    {
        var commandParts = command.Split(' ', 2).ToArray();
        var action = commandParts[0];

        if (action == "search")
        {
            var searchTerm = commandParts.ElementAtOrDefault(1);

            if (searchTerm != null)
            {
                var searchCommand = SearchCommand.CreateSearchCommand(_airportManager, _airlineManager, _flightManager, searchTerm);
                _invoker.ExecuteCommand(searchCommand);
            }
        }
        else if (action == "sort" && commandParts.Length >= 2)
        {
            commandParts = commandParts[1].Split().ToArray();
            var target = commandParts[0];
            var sortOrder = commandParts.ElementAtOrDefault(1);

            if (sortOrder != null)
                switch (target)
                {
                    case "airports":
                        var sortAirportsCommand = SortAirportsCommand.CreateSortAirportsCommand(_airportManager, sortOrder);
                        _invoker.ExecuteCommand(sortAirportsCommand);
                        break;

                    case "airlines":
                        var sortAirlinesCommand = SortAirlinesCommand.CreateSortAirlinesCommand(_airlineManager, sortOrder);
                        _invoker.ExecuteCommand(sortAirlinesCommand);
                        break;

                    case "flights":
                        var sortFlightsCommand = SortFlightsCommand.CreateSortFlightsCommand(_flightManager, sortOrder);
                        _invoker.ExecuteCommand(sortFlightsCommand);
                        break;

                    default:
                        Console.WriteLine(" Invalid command!");
                        break;
                }
        }
        else if (action == "exist" && commandParts.Length >= 2)
        {
            var airlineName = commandParts.ElementAtOrDefault(1);

            if (airlineName != null)
            {
                var existCommand = CheckAirportExistenceCommand.CreateCheckAirportExistenceCommand(_airportManager, airlineName);
                _invoker.ExecuteCommand(existCommand);
            }
        }
        else if (action == "list" && commandParts.Length >= 2)
        {
            commandParts = StringHelper.SplitBeforeLastElement(commandParts[1]);
            var inputData = commandParts.ElementAtOrDefault(0);
            var from = commandParts.ElementAtOrDefault(1);

            if (inputData != null && from != null)
            {
                var listCommand = ListDataCommand.CreateListDataCommand(_airportManager, inputData, from);
                _invoker.ExecuteCommand(listCommand);
            }
        }
        else if (action == "route" && commandParts.Length >= 2)
        {
            var commandArguments = commandParts[1].Split().ToArray();
            var commandAction = commandArguments.ElementAtOrDefault(0);

            if (commandAction == "new")
            {
                var routeNewCommand = RouteNewCommand.CreateRouteNewCommand(_routeManager);
                _invoker.ExecuteCommand(routeNewCommand);
            }
            else if (commandAction == "add" && commandArguments.Length == 2)
            {
                var flightId = commandArguments.ElementAtOrDefault(1);

                var flightToAdd = _flightManager.Flights.FirstOrDefault(x => x.Id == flightId);

                if (flightToAdd != null && _routeManager.Validate(flightToAdd))
                {
                    var routeAddCommand = RouteAddCommand.CreateRouteAddCommand(_routeManager, flightToAdd);
                    _invoker.ExecuteCommand(routeAddCommand);

                    Console.WriteLine($" Flight with ID '{flightId}' added to the route.");
                }
                else
                    Console.WriteLine($" Error: Flight does not exist.");
            }
            else if (commandAction == "remove")
                if (!_routeManager.IsEmpty())
                {
                    var routeRemoveCommand = RouteRemoveCommand.CreateRouteRemoveCommand(_routeManager);
                    _invoker.ExecuteCommand(routeRemoveCommand);

                    Console.WriteLine("Last flight removed from the route.");
                }
            if (commandAction == "print")
            {
                var routePrintCommand = RoutePrintCommand.CreateRoutePrintCommand(_routeManager);
                _invoker.ExecuteCommand(routePrintCommand);
            }
        }
        else if (action == "reserve" && commandParts.Length >= 2)
        {
            var commandArguments = commandParts[1].Split().ToArray();

            if (commandArguments.Length < 4)
                throw new Exception();

            var commandAction = commandArguments[0];
            var flightId = commandArguments[1];

            if (commandAction == "cargo" && commandArguments.Length >= 4)
            {
                var cargoWeight = double.Parse(commandArguments[2]);
                var cargoVolume = double.Parse(commandArguments[3]);

                var cargoReservation = new CargoReservation(flightId, cargoWeight, cargoVolume);

                var aircraftModel = _flightManager.GetAircraftModel(flightId);
                var aircraft = _aircraftManager.GetCargoAircraft(aircraftModel);

                if (aircraft != null && ReservationsManager.ValidateCargoReservation(cargoReservation, aircraft))
                {
                    var reserveCargoCommand = ReserveCargoCommand.CreateReserveCargoCommand(_reservationsManager, cargoReservation);
                    _invoker.ExecuteCommand(reserveCargoCommand);
                }
            }
            else if (commandAction == "ticket")
            {
                var seats = int.Parse(commandArguments[2]);
                var smallBaggaeCount = int.Parse(commandArguments[3]);
                var largeBaggaeCount = int.Parse(commandArguments[3]);

                var ticketReservation = new TicketReservation(flightId, seats, smallBaggaeCount, largeBaggaeCount);

                var aircraftModel = _flightManager.GetAircraftModel(flightId);
                var aircraft = _aircraftManager.GetPassengerAircraft(aircraftModel);

                if (ReservationsManager.ValidateTicketReservation(ticketReservation, aircraft!))
                {
                    var reserveTicketCommand = ReserveTicketCommand.CreateTicketCommand(_reservationsManager, ticketReservation);
                    _invoker.ExecuteCommand(reserveTicketCommand);
                }
            }
        }
        else
            Console.WriteLine(" Inavalid command!");
    }
}