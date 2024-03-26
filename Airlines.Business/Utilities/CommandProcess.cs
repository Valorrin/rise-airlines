using Airlines.Business.Managers;
using Airlines.Business.Models;

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
                airportManager.Search(searchTerm);
                airlineManager.Search(searchTerm);
                flightManager.Search(searchTerm);
                return;
            }
        }
        else if (action == "sort" && commandParts.Length >= 2)
        {
            commandParts = commandParts[1].Split().ToArray();
            var target = commandParts[0];
            var sortOrder = commandParts.ElementAtOrDefault(1);

            switch (target)
            {
                case "airports":
                    if (sortOrder == "descending")
                    {
                        var names = airportManager.SortDescByName();
                        Console.WriteLine(string.Join(", ", names));
                    }
                    else
                    {
                        var names = airportManager.SortByName();
                        Console.WriteLine(string.Join(", ", names));
                    }
                    break;

                case "airlines":
                    if (sortOrder == "descending")
                    {
                        var names = airlineManager.SortDescByName();
                        Console.WriteLine(string.Join(", ", names));
                    }
                    else
                    {
                        var names = airlineManager.SortByName();
                        Console.WriteLine(string.Join(", ", names));
                    }
                    break;

                case "flights":
                    if (sortOrder == "descending")
                    {
                        var ids = flightManager.SortDescById();
                        Console.WriteLine(string.Join(", ", ids));
                    }
                    else
                    {
                        var ids = flightManager.SortById();
                        Console.WriteLine(string.Join(", ", ids));
                    }
                    break;
                default:
                    Console.WriteLine(" Invalid command!");
                    break;
            }
        }
        else if (action == "exist" && commandParts.Length >= 2)
        {
            var airlineName = commandParts[1];

            var result = airportManager.Exist(airlineName);

            Console.WriteLine(result);
        }
        else if (action == "list" && commandParts.Length >= 2)
        {
            commandParts = StringHelper.SplitBeforeLastElement(commandParts[1]);
            var inputData = commandParts[0];
            var from = commandParts[1];

            airportManager.ListData(inputData, from);
        }
        else if (action == "route" && commandParts.Length >= 2)
        {
            var commandArguments = commandParts[1].Split().ToArray();
            var commandAction = commandArguments[0];

            if (commandAction == "new")
                routeManager.Routes.Clear();
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
            if (!routeManager.IsEmpty())
            {
                foreach (var flight in routeManager.Routes)
                {
                    Console.WriteLine($"  Flight ID: {flight.Id}");
                    Console.WriteLine($"  Departure Airport ID: {flight.DepartureAirport}");
                    Console.WriteLine($"  Arrival Airport ID: {flight.ArrivalAirport}");
                    Console.WriteLine($"  Aircraft Model: {flight.AircraftModel}\n");
                }
            }
            else
                Console.WriteLine(" Route is empty.");
        }
        else if (action == "reserve" && commandParts.Length >= 2)
        {
            var commandArguments = commandParts[1].Split().ToArray();
            var commandAction = commandArguments[0];

            if (commandAction == "cargo")
            {
                var flightId = commandArguments[1];
                var cargoWeight = double.Parse(commandArguments[2]);
                var cargoVolume = double.Parse(commandArguments[3]);

                var cargoReservation = new CargoReservation
                {
                    FlightId = flightId,
                    CargoWeight = cargoWeight,
                    CargoVolume = cargoVolume
                };

                var aircraftModel = flightManager.GetFlightModel(flightId);
                var aircraft = aircraftManager.GetCargoAircraft(aircraftModel);

                if (ReservationsManager.ValidateCargoReservation(cargoReservation, aircraft!))
                {
                    reservationsManager.Add(cargoReservation);
                }
            }
            else
                Console.WriteLine(" Inavalid command!");
        }
    }
}