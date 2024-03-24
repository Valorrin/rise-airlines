
namespace Airlines.Business;
public class CommandProcess
{
    public static void ExecuteCommand(string command, AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager)
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
    }
}