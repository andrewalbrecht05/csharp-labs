using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers;

public class Task2Controller : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}