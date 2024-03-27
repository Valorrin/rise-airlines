using Airlines.Business.Managers;

namespace Airlines.Business.Commands;
public class CheckAirportExistenceCommand : ICommand
{
    private readonly AirportManager _airportManager;
    private readonly string _airlineName;

    public CheckAirportExistenceCommand(AirportManager airportManager, string airlineName)
    {
        _airportManager = airportManager;
        _airlineName = airlineName;
    }

    public void Execute()
    {
        var result = _airportManager.Exist(_airlineName);
        Console.WriteLine(result);
    }

    public static CheckAirportExistenceCommand CreateCheckAirportExistenceCommand(AirportManager airportManager, string airlineName) => new(airportManager, airlineName);
}