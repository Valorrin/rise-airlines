using Airlines.Service.Dto;
using Airlines.Service.Services.AirportService;
using Airlines.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class AirportsController : Controller
{
    private readonly IAirportService _airportService;

    public AirportsController(IAirportService airportService) => _airportService = airportService;

    [HttpGet]
    public async Task<IActionResult> Index(string searchTerm, string filter)
    {
        List<AirportDto>? airports;

        if (string.IsNullOrEmpty(searchTerm))
        {
            airports = await _airportService.GetAllAirportsAsync();
        }
        else
        {
            airports = await _airportService.GetAllAirportsAsync(filter, searchTerm);
        }

        var viewModel = new AirportViewModel { Airports = airports };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddAirport(AirportDto model)
    {

        if (await _airportService.IsAirportCodeUniqueAsync(model.Code!))
        {
            ModelState.AddModelError("Code", "The code must be unique.");
        }

        if (await _airportService.IsAirportNameUniqueAsync(model.Name!))
        {
            ModelState.AddModelError("Name", "The name must be unique");
        }

        if (!ModelState.IsValid)
        {
            var airports = await _airportService.GetAllAirportsAsync();
            var viewModel = new AirportViewModel { Airports = airports };

            return View("Index", viewModel);
        }

        await _airportService.AddAirportAsync(model);
        return RedirectToAction(nameof(Index));
    }
}
