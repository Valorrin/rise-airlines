using Microsoft.AspNetCore.Mvc;

namespace Airlines.Web.Controllers;
public class AirlinesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
