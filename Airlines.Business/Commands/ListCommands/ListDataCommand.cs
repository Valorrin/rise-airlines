using Airlines.Business.Managers;

namespace Airlines.Business.Commands.ListingCommands;
public class ListDataCommand : ICommand
{
    private readonly AirportManager _airportManager;
    private readonly string _inputData;
    private readonly string _from;

    public ListDataCommand(AirportManager airportManager, string inputData, string from)
    {
        _airportManager = airportManager;
        _inputData = inputData;
        _from = from;
    }

    public void Execute() => _airportManager.ListData(_inputData, _from);
}