﻿using Airlines.Business.Commands.SearchCommands;
using Airlines.Business.Managers;

namespace Airlines.UnitTests.CommandTests;

[Collection("Sequential")]
public class SearchCommandTests
{

    [Fact]
    public void CreateSearchCommand_ReturnsInstance()
    {
        var airportManager = new AirportManager();
        var airlineManager = new AirlineManager();
        var flightManager = new FlightManager();
        var searchTerm = "test";

        var command = new SearchCommand(airportManager, airlineManager, flightManager, searchTerm);

        Assert.NotNull(command);
        _ = Assert.IsType<SearchCommand>(command);
    }
}