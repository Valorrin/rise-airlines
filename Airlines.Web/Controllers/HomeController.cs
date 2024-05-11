using Airlines.Service.Services.AirlineService;
using Airlines.Service.Services.AirportService;
using Airlines.Service.Services.FlightService;
using Airlines.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class HomeController : Controller
{
    private readonly IAirlineService _airlineService;
    private readonly IAirportService _airportService;
    private readonly IFlightService _flightService;

    public HomeController(IAirlineService airlineService, IAirportService airportService, IFlightService flightService)
    {
        _airlineService = airlineService;
        _airportService = airportService;
        _flightService = flightService;
    }

    public async Task<IActionResult> Index()
    {
        var airlinesCount = await _airlineService.GetAirlinesCountAsync();
        var airportsCount = await _airportService.GetAirportsCountAsync();
        var flightsCount = await _flightService.GetFlightsCountAsync();

        var viewModel = new HomeViewModel
        {
            AirlinesCount = airlinesCount,
            AirportsCount = airportsCount,
            FlightsCount = flightsCount
        };

        return View(viewModel);
    }
}
