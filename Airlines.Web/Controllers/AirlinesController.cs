using Airlines.Service.Dto;
using Airlines.Service.Services.AirlineService;
using Airlines.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class AirlinesController : Controller
{
    private readonly IAirlineService _airlineService;

    public AirlinesController(IAirlineService airlineService) => _airlineService = airlineService;

    public async Task<IActionResult> Index()
    {
        var airlines = await _airlineService.GetAllAirlinesAsync();
        var viewModel = new AirlinesViewModel { Airlines = airlines };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddAirline(AirlineDto model)
    {
        if (_airlineService.IsAirlineNameLengthValid(model.Name))
        {
            ModelState.AddModelError("Name", "The airport name must be up to 6 characters long.");
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
