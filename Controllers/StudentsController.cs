using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
namespace Lab3.Controllers;

public class StudentsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(DataProvider.Students);
    }
}