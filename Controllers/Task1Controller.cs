using Lab3.Models;
using static Lab3.Models.DataProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab3.Controllers;

public class Task1Controller : Controller
{
    private List<SelectListItem>? _availableMarks;

    private void GetMarks()
    {
        if (_availableMarks is not null) return;
        var allMarks = new SortedSet<int>();
        foreach (var ratingField in Ratings)
            allMarks.Add(GetMark(ratingField.pointsFirstModule, ratingField.pointsSecondModule));

        _availableMarks = new List<SelectListItem> { new() { Text = "Усі оцінки", Value = "Усі оцінки" } };
        
        foreach (var mark in allMarks)
            _availableMarks.Add(new SelectListItem(mark.ToString(), mark.ToString()));
    }

    private static int GetMark(int firstMark, int secondMark)
    {
        var averageMark = (firstMark + secondMark) / 2f;
        return averageMark switch
        {
            < 35 => 1,
            < 60 => 2,
            < 74 => 3,
            < 90 => 4,
            _ => 5
        };
    }


    public IActionResult Index(int? mark)
    {
        GetMarks();
        var tuple = new List<(string name, string? group, int? mark)>();
        var sortedStudents = new List<Student>(Students);
        sortedStudents.Sort((student1, student2) => String.Compare(student1.Name, student2.Name));

        foreach (var student in sortedStudents)
        {
            var rating = RatingsDict.GetValueOrDefault(student.Id)!;
            var aveMark = (rating.pointsFirstModule + rating.pointsSecondModule) / 2;

            if (mark is null || GetMark(rating.pointsFirstModule, rating.pointsSecondModule) == mark)
                tuple.Add((student.Name, student.Group, aveMark));
        }

        var viewModel = new Task1ViewModel
            { Tuple = tuple, Marks = _availableMarks, MarkString = mark?.ToString() ?? "Усі оцінки" };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Index(Task1ViewModel task1ViewModel)
    {
        if (!ModelState.IsValid)
        {
            RedirectToAction("Index");
        }

        int? mark = int.TryParse(task1ViewModel.MarkString, out var markValue) ? markValue : null;
        return Index(mark);
    }
}