using Airlines.Service.Dto;
using Airlines.Service.Services.AirportService;
using Airlines.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class AirportsController : Controller
{
    private readonly IAirportService _airportService;

    public AirportsController(IAirportService airportService) => _airportService = airportService;

    public async Task<IActionResult> Index()
    {
        var airports = await _airportService.GetAllAirportsAsync();
        var viewModel = new AirportViewModel { Airports = airports };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddAirport(AirportDto model)
    {
        await _airportService.AddAirportAsync(model);
        return RedirectToAction(nameof(Index));
    }
}
