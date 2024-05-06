using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class AirportsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
