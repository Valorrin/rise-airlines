﻿using Airlines.Business.Managers;

namespace Airlines.Business.Commands.SearchCommands;
public class CheckAirportExistenceCommand : ICommand
{
    private readonly AirportManager _airportManager;
    private readonly string _airlineName;

    public CheckAirportExistenceCommand(AirportManager airportManager, string airlineName)
    {
        _airportManager = airportManager;
        _airlineName = airlineName;
    }

    public void Execute() => _airportManager.Exist(_airlineName);
}