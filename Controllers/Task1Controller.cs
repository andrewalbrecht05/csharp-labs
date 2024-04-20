using Lab3.Models;
using static Lab3.Models.DataProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab3.Controllers;

public class Task1Controller : Controller
{
    private List<SelectListItem>? _availableMarks;
    
    private void GetYears()
    {
        if (_availableMarks is null)
        {
            var allMarks = new SortedSet<int>();
            /*foreach (var ratingField in Ratings)
            {
                if (ratingField.pointsFirstModule + )
                    allMarks.Add(visit.Date.Value.Year);
            }*/

            _availableMarks = new List<SelectListItem> { new() { Text = "Усі оцінки", Value = "Усі оцінки" } };
            foreach (var mark in allMarks)
            {
                _availableMarks.Add(new SelectListItem(mark.ToString(), mark.ToString()));
            }
        }
    }
    public IActionResult Index()
    {
        return View();
    }
}