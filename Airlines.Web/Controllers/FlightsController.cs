using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class FlightsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
