using Airlines.Service.Dto;
using Airlines.Service.Services.FlightService;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class FlightsController : Controller
{
    private readonly IFlightService _flightService;

    public FlightsController(IFlightService flightService) => _flightService = flightService;

    public async Task<IActionResult> Index()
    {
        var flights = await _flightService.GetAllFlightsAsync();
        return View(flights);
    }

    [HttpPost]
    public async Task<IActionResult> AddFlight(FlightDto model)
    {
        await _flightService.AddFlightAsync(model);
        return RedirectToAction(nameof(Index));
    }
}
