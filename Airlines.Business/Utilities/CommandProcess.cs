using Airlines.Business.Commands.ListingCommands;
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
            }
        }
        else if (action == "route" && commandParts.Length >= 2)
        {
            var commandArguments = commandParts[1].Split().ToArray();
            var commandAction = commandArguments[0];

            if (commandAction == "new")
            {
                routeManager.Routes.Clear();
            }
            else if (commandAction == "add" && commandArguments.Length == 2)
            {
                var flightId = commandArguments[1];

                var flightToAdd = flightManager.Flights.FirstOrDefault(x => x.Id == flightId);

                if (flightToAdd != null && routeManager.Validate(flightToAdd))
                {
                    routeManager.AddFlight(flightToAdd);
                    Console.WriteLine($" Flight with ID '{flightId}' added to the route.");
                }
                else
                    Console.WriteLine($" Error: Flight does not exist.");
            }
            else if (commandAction == "remove")
                if (!routeManager.IsEmpty())
                {
                    routeManager.RemoveFlight();
                    Console.WriteLine("Last flight removed from the route.");
                }
            if (commandAction == "print")
            {
                if (routeManager.Routes == null)
                {
                    Console.WriteLine(" Route is empty.");
                }
                else
                {
                    foreach (var flight in routeManager.Routes)
                    {
                        Console.WriteLine($"  Flight ID: {flight.Id}");
                        Console.WriteLine($"  Departure Airport ID: {flight.DepartureAirport}");
                        Console.WriteLine($"  Arrival Airport ID: {flight.ArrivalAirport}");
                        Console.WriteLine($"  Aircraft Model: {flight.AircraftModel}\n");
                    }
                }
            }
        }
        else if (action == "reserve" && commandParts.Length >= 2)
        {
            var commandArguments = commandParts[1].Split().ToArray();
            var commandAction = commandArguments[0];
            var flightId = commandArguments[1];

            if (commandAction == "cargo")
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