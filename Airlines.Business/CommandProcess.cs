using System.Linq;

namespace Airlines.Business;
public class CommandProcess
{
    public static void ProcessCommand(string command, AirportManager airportManager, AirlineManager airlineManager, FlightManager flightManager)
    {
        var commandParts = command.Split().ToArray();
        var action = commandParts[0];

        if (action == "search")
        {
            var searchTerm = commandParts.ElementAtOrDefault(1);
            if (searchTerm != null)
            {
                //var matchingAirports = airportManager.Airports.Where(airport => airport.Contains(searchTerm)).ToList();
                airportManager.Search(searchTerm);
                airlineManager.Search(searchTerm);
                flightManager.Search(searchTerm);
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
    }
}
