using Lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers;

public class RatingsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(DataProvider.Ratings);
    }
}