
namespace Airlines.Business;
public class CommandProcess
{
    public static void ExecuteCommand(string command, AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager, RouteManager routeManager)
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
                if (flightManager.Search(searchTerm))
                {
                    Console.WriteLine($" {searchTerm} is Flight name.");
                }
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
            var commandPartsSplit = commandParts[1].Split().ToArray();
            var commandTarget = commandPartsSplit[0];

            if (commandTarget == "new")
            {
                routeManager.Routes.Clear();
            }
            else if (commandTarget == "add" && commandPartsSplit.Length == 2)
            {
                var flightId = commandPartsSplit[1];

                if (!flightManager.Search(flightId))
                {
                    Console.WriteLine($"Flight with id: {flightId} does not exists!");
                }
                else
                {
                    var flight = flightManager.Flights.FirstOrDefault(x => x.Id == flightId);

                    if (routeManager.Validate(flight!))
                    {
                        routeManager.AddFlight(flight!);
                    }
                }
            }
            else if (commandTarget == "remove")
            {
                routeManager.RemoveFlight();
            }
            else if (commandTarget == "print")
            {
                Console.Write($"Route: \n");
                foreach (var flight in routeManager.Routes)
                {
                    Console.WriteLine($" Flight id: {flight.Id}");
                    Console.WriteLine($" Departure Airport id: {flight.DepartureAirport}");
                    Console.WriteLine($" Arrival Airport id: {flight.ArrivalAirport}\n");
                }
            }
        }
        else
        {
            Console.WriteLine(" Inavalid command!");
        }
    }
}