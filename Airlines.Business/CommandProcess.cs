using System.Linq;

namespace Airlines.Business;
public class CommandProcess
{
    public static void ProcessCommand(string command, AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager)
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
                        airportManager.SortDesc();
                    }
                    else
                    {
                        airlineManager.Sort();
                    }
                    break;

                case "airlines":
                    if (sortOrder == "descending")
                    {
                        airlineManager.SortDesc();
                    }
                    else
                    {
                        airlineManager.Sort();
                    }
                    break;

                case "flights":
                    if (sortOrder == "descending")
                    {
                        flightManager.SortDesc();
                    }
                    else
                    {
                        flightManager.Sort();
                    }
                    break;
                default:
                    Console.WriteLine(" Invalid command!");
                    break;
            }
        }
        else if (action == "exist" && commandParts.Length >= 2)
        {
            var airlineName = commandParts.ElementAtOrDefault(1);

            var result = airportManager.Exist(airlineName);

            Console.WriteLine(result);
        }
        else if (action == "list" && commandParts.Length >= 2)
        {
            commandParts = SplitBeforeLastElement(commandParts[1]);
            var inputData = commandParts[0];
            var from = commandParts[1];

            airportManager.ListData(inputData, from);
        }
    }
    private static string[] SplitBeforeLastElement(string input)
    {
        var lastIndex = input.LastIndexOf(' ');
        if (lastIndex != -1)
        {
            var firstPart = input.Substring(0, lastIndex);
            var lastPart = input.Substring(lastIndex + 1);

            return [firstPart, lastPart];
        }
        else
        {
            Console.WriteLine("String cannot be split before the last element.");
            return null;
        }
    }
}
