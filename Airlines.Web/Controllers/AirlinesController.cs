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
        await _airlineService.AddAirlineAsync(model);
        return RedirectToAction(nameof(Index));
    }
}
