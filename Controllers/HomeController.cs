using Microsoft.AspNetCore.Mvc;

namespace safonenko.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
