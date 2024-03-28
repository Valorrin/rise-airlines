using Airlines.Business.Commands.ListingCommands;
using Airlines.Business.Commands.RouteCommands;
using Airlines.Business.Commands.SearchCommands;
using Airlines.Business.Commands.SortCommands;
using Airlines.Business.Managers;
using Airlines.Business.Models.Reservations;

namespace Airlines.Business.Utilities;
public class CommandProcess
{
    public static void ExecuteCommand(string command, AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager,
        RouteManager routeManager, AircraftManager aircraftManager, ReservationsManager reservationsManager)
    {
        var commandParts = command.Split(' ', 2).ToArray();
        var action = commandParts[0];

        if (action == "search")
        {
            var searchTerm = commandParts.ElementAtOrDefault(1);

            if (searchTerm != null)
            {
                var searchCommand = SearchCommand.CreateSearchCommand(airportManager, airlineManager, flightManager, searchTerm);
                searchCommand.Execute();
            }
        }
        else if (action == "sort" && commandParts.Length >= 2)
        {
            commandParts = commandParts[1].Split().ToArray();
            var target = commandParts[0];
            var sortOrder = commandParts.ElementAtOrDefault(1);

            if (sortOrder != null)
            {
                switch (target)
                {
                    case "airports":
                        var sortAirportsCommand = SortAirportsCommand.CreateSortAirportsCommand(airportManager, sortOrder);
                        sortAirportsCommand.Execute();
                        break;

                    case "airlines":
                        var sortAirlinesCommand = SortAirlinesCommand.CreateSortAirlinesCommand(airlineManager, sortOrder);
                        sortAirlinesCommand.Execute();
                        break;

                    case "flights":
                        var sortFlightsCommand = SortFlightsCommand.CreateSortFlightsCommand(flightManager, sortOrder);
                        sortFlightsCommand.Execute();
                        break;

                    default:
                        Console.WriteLine(" Invalid command!");
                        break;
                }
            }
        }
        else if (action == "exist" && commandParts.Length >= 2)
        {
            var airlineName = commandParts.ElementAtOrDefault(1);

            if (airlineName != null)
            {
                var existCommand = CheckAirportExistenceCommand.CreateCheckAirportExistenceCommand(airportManager, airlineName);
                existCommand.Execute();
            }
        }
        else if (action == "list" && commandParts.Length >= 2)
        {
            commandParts = StringHelper.SplitBeforeLastElement(commandParts[1]);
            var inputData = commandParts.ElementAtOrDefault(0);
            var from = commandParts.ElementAtOrDefault(1);

            if (inputData != null && from != null)
            {
                var listCommand = ListDataCommand.CreateListDataCommand(airportManager, inputData, from);
                listCommand.Execute();
            }
        }
        else if (action == "route" && commandParts.Length >= 2)
        {
            var commandArguments = commandParts[1].Split().ToArray();
            var commandAction = commandArguments.ElementAtOrDefault(0);

            if (commandAction == "new")
            {
                var routeNewCommand = RouteNewCommand.CreateRouteNewCommand(routeManager);
                routeNewCommand.Execute();
            }
            else if (commandAction == "add" && commandArguments.Length == 2)
            {
                var flightId = commandArguments.ElementAtOrDefault(1);

                var flightToAdd = flightManager.Flights.FirstOrDefault(x => x.Id == flightId);

                if (flightToAdd != null && routeManager.Validate(flightToAdd))
                {
                    var routeAddCommand = RouteAddCommand.CreateRouteAddCommand(routeManager, flightToAdd);
                    routeAddCommand.Execute();

                    Console.WriteLine($" Flight with ID '{flightId}' added to the route.");
                }
                else
                    Console.WriteLine($" Error: Flight does not exist.");
            }
            else if (commandAction == "remove")
            {
                if (!routeManager.IsEmpty())
                {
                    var routeRemoveCommand = RouteRemoveCommand.CreateRouteRemoveCommand(routeManager);
                    routeRemoveCommand.Execute();

                    Console.WriteLine("Last flight removed from the route.");
                }
            }
            if (commandAction == "print")
            {
                var routePrintCommand = RoutePrintCommand.CreateRoutePrintCommand(routeManager);
                routePrintCommand.Execute();
            }
        }
        else if (action == "reserve" && commandParts.Length >= 2)
        {
            var commandArguments = commandParts[1].Split().ToArray();
            var commandAction = commandArguments.ElementAtOrDefault(0);
            var flightId = commandArguments.ElementAtOrDefault(1);

            if (commandAction == "cargo" && commandArguments.Length >= 4)
            {
                var cargoWeight = double.Parse(commandArguments[2]);
                var cargoVolume = double.Parse(commandArguments[3]);

                var cargoReservation = new CargoReservation(flightId, cargoWeight, cargoVolume);

                var aircraftModel = flightManager.GetAircraftModel(flightId);
                var aircraft = aircraftManager.GetCargoAircraft(aircraftModel);

                if (ReservationsManager.ValidateCargoReservation(cargoReservation, aircraft!))
                {
                    reservationsManager.Add(cargoReservation);
                }
            }
            else if (commandAction == "ticket")
            {
                var seats = int.Parse(commandArguments[2]);
                var smallBaggaeCount = int.Parse(commandArguments[3]);
                var largeBaggaeCount = int.Parse(commandArguments[3]);

                var ticketReservation = new TicketReservation(flightId, seats, smallBaggaeCount, largeBaggaeCount);

                var aircraftModel = flightManager.GetAircraftModel(flightId);
                var aircraft = aircraftManager.GetPassengerAircraft(aircraftModel);

                if (ReservationsManager.ValidateTicketReservation(ticketReservation, aircraft!))
                {
                    reservationsManager.Add(ticketReservation);
                }
            }
        }
        else
        {
            Console.WriteLine(" Inavalid command!");
        }
    }
}