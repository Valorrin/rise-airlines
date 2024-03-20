
namespace Airlines.Business;
public class CommandProcess
{
    public static void ProcessCommand(string command, AirportManager airports, AirlineManager airlines, FlightManager flights)
    {
        var commandParts = command.Split().ToArray();
        var action = commandParts[0];

        if (action == "search")
        {
            var searchTerm = commandParts.ElementAtOrDefault(1);
            if (searchTerm != null)
            {
                airports.Search(searchTerm);
                airlines.Search(searchTerm);
                flights.Search(searchTerm);
                return;
            }
        }
        else if (action == "sort" && commandParts.Length >= 2)
        {
            var target = commandParts[1];
            var sortOrder = commandParts.ElementAtOrDefault(2);

            switch (target)
            {
                case "airports":
                    if (sortOrder == "descending")
                    {
                        airports.SortDesc();
                    }
                    else
                    {
                        airports.Sort();
                    }
                    break;

                case "airlines":
                    if (sortOrder == "descending")
                    {
                        airlines.SortDesc();
                    }
                    else
                    {
                        airlines.Sort();
                    }
                    break;

                case "flights":
                    if (sortOrder == "descending")
                    {
                        flights.SortDesc();
                    }
                    else
                    {
                        flights.Sort();
                    }
                    break;
                default:
                    Console.WriteLine(" Invalid command!");
                    break;
            }
        }
    }
}
