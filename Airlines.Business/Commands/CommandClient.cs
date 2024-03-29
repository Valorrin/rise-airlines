using Airlines.Business.Commands.BatchCommands;
using Airlines.Business.Commands.ListingCommands;
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
    private readonly BatchManager _batchManager;

    public CommandClient(CommandInvoker invoker,
                             AirportManager airportManager,
                             AirlineManager airlineManager,
                             FlightManager flightManager,
                             RouteManager routeManager,
                             AircraftManager aircraftManager,
                             ReservationsManager reservationsManager,
                             BatchManager batchManager)
    {
        _invoker = invoker;
        _airportManager = airportManager;
        _airlineManager = airlineManager;
        _flightManager = flightManager;
        _routeManager = routeManager;
        _aircraftManager = aircraftManager;
        _reservationsManager = reservationsManager;
        _batchManager = batchManager;
    }

    public void ProcessCommand(string command, bool batchMode)
    {
        var commandParts = command.Split(' ', 2).ToArray();
        var action = commandParts[0];

        if (action == "search")
        {
            var searchTerm = commandParts.ElementAtOrDefault(1);
            var searchCommand = SearchCommand.CreateSearchCommand(_airportManager, _airlineManager, _flightManager, searchTerm);

            if (batchMode)
            {
                _batchManager.AddCommand(searchCommand);
            }
            else
            {
                _invoker.ExecuteCommand(searchCommand);
            }
        }
        else if (action == "sort")
        {
            commandParts = commandParts[1].Split().ToArray();
            var target = commandParts[0];
            var sortOrder = commandParts[1];

            if (target == "airports")
            {
                var sortAirportsCommand = SortAirportsCommand.CreateSortAirportsCommand(_airportManager, sortOrder);

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
                var sortAirlinesCommand = SortAirlinesCommand.CreateSortAirlinesCommand(_airlineManager, sortOrder);

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
                var sortFlightsCommand = SortFlightsCommand.CreateSortFlightsCommand(_flightManager, sortOrder);

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
        else if (action == "exist")
        {
            var airportName = commandParts[1];

            var existCommand = CheckAirportExistenceCommand.CreateCheckAirportExistenceCommand(_airportManager, airportName);

            if (batchMode)
            {
                _batchManager.AddCommand(existCommand);
            }
            else
            {
                _invoker.ExecuteCommand(existCommand);
            }
        }
        else if (action == "list")
        {
            commandParts = StringHelper.SplitBeforeLastElement(commandParts[1]);
            var inputData = commandParts[0];
            var from = commandParts[1];

            var listCommand = ListDataCommand.CreateListDataCommand(_airportManager, inputData, from);
            if (batchMode)
            {
                _batchManager.AddCommand(listCommand);
            }
            else
            {
                _invoker.ExecuteCommand(listCommand);
            }
        }
        else if (action == "route")
        {
            var commandArguments = commandParts[1].Split().ToArray();
            var commandAction = commandArguments[0];

            if (commandAction == "new")
            {
                var routeNewCommand = RouteNewCommand.CreateRouteNewCommand(_routeManager);
                if (batchMode)
                {
                    _batchManager.AddCommand(routeNewCommand);
                }
                else
                {
                    _invoker.ExecuteCommand(routeNewCommand);
                }
            }
            else if (commandAction == "add")
            {
                var flightId = commandArguments[1];

                var flightToAdd = _flightManager.Flights.FirstOrDefault(x => x.Id == flightId);

                if (flightToAdd != null && _routeManager.Validate(flightToAdd))
                {
                    var routeAddCommand = RouteAddCommand.CreateRouteAddCommand(_routeManager, flightToAdd);
                    if (batchMode)
                    {
                        _batchManager.AddCommand(routeAddCommand);
                    }
                    else
                    {
                        _invoker.ExecuteCommand(routeAddCommand);
                    }

                    Console.WriteLine($" Flight with ID '{flightId}' added to the route.");
                }
                else
                    Console.WriteLine($" Error: Flight does not exist.");
            }
            else if (commandAction == "remove")
                if (!_routeManager.IsEmpty())
                {
                    var routeRemoveCommand = RouteRemoveCommand.CreateRouteRemoveCommand(_routeManager);
                    if (batchMode)
                    {
                        _batchManager.AddCommand(routeRemoveCommand);
                    }
                    else
                    {
                        _invoker.ExecuteCommand(routeRemoveCommand);
                    }

                    Console.WriteLine("Last flight removed from the route.");
                }
            if (commandAction == "print")
            {
                var routePrintCommand = RoutePrintCommand.CreateRoutePrintCommand(_routeManager);
                if (batchMode)
                {
                    _batchManager.AddCommand(routePrintCommand);
                }
                else
                {
                    _invoker.ExecuteCommand(routePrintCommand);
                }
            }
        }
        else if (action == "reserve")
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
                    if (batchMode)
                    {
                        _batchManager.AddCommand(reserveCargoCommand);
                    }
                    else
                    {
                        _invoker.ExecuteCommand(reserveCargoCommand);
                    }
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
                    if (batchMode)
                    {
                        _batchManager.AddCommand(reserveTicketCommand);
                    }
                    else
                    {
                        _invoker.ExecuteCommand(reserveTicketCommand);
                    }
                }
            }
        }
        else if (action == "batch")
        {
            var commandArguments = commandParts[1].Split().ToArray();
            var commandAction = commandArguments.ElementAtOrDefault(0);

            if (commandAction == "start")
            {
                var batchStartCommand = BatchStartCommand.CreateBatchStartCommand(_batchManager);

                if (batchMode)
                {
                    _batchManager.AddCommand(batchStartCommand);
                }
                else
                {
                    _invoker.ExecuteCommand(batchStartCommand);
                }
            }
            else if (commandAction == "run")
            {
                var batchRunCommand = BatchRunCommand.CreateBatchRunCommand(_batchManager, _invoker);

                if (batchMode)
                {
                    _batchManager.AddCommand(batchRunCommand);
                }
                else
                {
                    _invoker.ExecuteCommand(batchRunCommand);
                }
            }
            else if (commandAction == "cancel")
            {
                var batchCancelCommand = BatchCancelCommand.CreateBatchCancelCommand(_batchManager);

                if (batchMode)
                {
                    _batchManager.AddCommand(batchCancelCommand);
                }
                else
                {
                    _invoker.ExecuteCommand(batchCancelCommand);
                }
            }
        }
    }
}