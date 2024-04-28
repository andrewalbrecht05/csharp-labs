using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using static Lab3.Models.DataProvider;

namespace Lab3.Controllers;

public class Task2Controller : Controller
{
    // GET
    public IActionResult Index()
    {
        var idAverages = new Dictionary<int, (int,int)>();
        
        foreach (var rating in Ratings)
        {
            if (!idAverages.ContainsKey(rating.Id))
            {
                idAverages[rating.Id] = (0,0);
            }

            var x = idAverages[rating.Id];
            idAverages[rating.Id] = (x.Item1 + rating.PointsFirstModule, x.Item2 + 1);
        }

        // Find the maximum of the averages
        var maxAverage = int.MinValue;
        foreach (var (sum,count) in idAverages.Values)
        {
            maxAverage = int.Max(maxAverage, sum / count);
        }

        var studentNames = new List<string>();

        foreach (var (k,v) in idAverages)
        {
            if (v.Item1 / v.Item2 == maxAverage)
                studentNames.Add(StudentsDict[k].Name);
        }
        var viewModel = new Task2ViewModel
        {
            StudentNames = studentNames,
            MaxRating = maxAverage,
        };
        return View(viewModel);
    }
}