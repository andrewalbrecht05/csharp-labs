using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using static Lab3.Models.DataProvider;
namespace Lab3.Controllers;

public class Task2Controller : Controller
{
    // GET
    public IActionResult Index()
    {
        var maxRating = Ratings.Max(rating => rating.pointsFirstModule);
        var studentNames = new List<string>();
        
        foreach (var rating in Ratings) {
            if (rating.pointsFirstModule == maxRating)
                studentNames.Add(rating.Name);
        }
        
        var viewModel = new Task2ViewModel
        {
            StudentNames = studentNames,
            MaxRating = maxRating
        };

        return View(viewModel);
    }
}