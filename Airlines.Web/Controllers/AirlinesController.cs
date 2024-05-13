using Airlines.Service.Dto;
using Airlines.Service.Services.AirlineService;
using Airlines.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class AirlinesController : Controller
{
    private readonly IAirlineService _airlineService;

    public AirlinesController(IAirlineService airlineService) => _airlineService = airlineService;

    [HttpGet]
    public async Task<IActionResult> Index(string searchTerm, string filter)
    {
        var airlines = await _airlineService.GetAllAirlinesAsync();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            airlines = await _airlineService.GetAllAirlinesAsync(filter, searchTerm);
        }

        var viewModel = new AirlinesViewModel { Airlines = airlines };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddAirline(AirlineDto model)
    {
        if (await _airlineService.IsAirlineNameUniqueAsync(model.Name!))
        {
            ModelState.AddModelError("Name", "The name must be unique");
        }

        if (!ModelState.IsValid)
        {
            var airlines = await _airlineService.GetAllAirlinesAsync();
            var viewModel = new AirlinesViewModel { Airlines = airlines };

            return View("Index", viewModel);
        }

        await _airlineService.AddAirlineAsync(model);
        return RedirectToAction(nameof(Index));
    }
}