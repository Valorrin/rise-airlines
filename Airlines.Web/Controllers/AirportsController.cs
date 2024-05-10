using Airlines.Service.Dto;
using Airlines.Service.Services.AirportService;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class AirportsController : Controller
{
    private readonly IAirportService _airportService;

    public AirportsController(IAirportService airportService) => _airportService = airportService;

    public async Task<IActionResult> Index()
    {
        var airports = await _airportService.GetAllAirportsAsync();
        return View(airports);
    }

    [HttpPost]
    public async Task<IActionResult> AddAirport(AirportDto model)
    {
        await _airportService.AddAirportAsync(model);
        return RedirectToAction(nameof(Index));
    }
}
