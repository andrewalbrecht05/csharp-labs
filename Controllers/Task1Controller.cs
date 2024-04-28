using Lab3.Models;
using static Lab3.Models.DataProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab3.Controllers;

public class Task1Controller : Controller
{
    private List<SelectListItem>? _availableSubjects;

    private void GetSubjects()
    {
        if (_availableSubjects is not null) return;

        var allSubjects = new SortedSet<string>();
        foreach (var ratingField in Ratings)
            allSubjects.Add(ratingField.SubjectName);

        _availableSubjects = new List<SelectListItem> { new() { Text = "Усі предмети", Value = "Усі предмети" } };

        foreach (var subject in allSubjects)
            _availableSubjects.Add(new SelectListItem(subject, subject));
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


    public IActionResult Index(string? selectedSubject)
    {
        GetSubjects();
        var tuple = new List<(string name, string? group, int? mark)>();
        var sortedStudents = new List<Student>(Students);
        sortedStudents.Sort((student1, student2) => String.Compare(student1.Name, student2.Name));

        foreach (var student in sortedStudents)
        {
            var rating = RatingsDict.GetValueOrDefault((student.Id, selectedSubject));
            //var aveMark = (rating.PointsFirstModule + rating.PointsSecondModule) / 2;
            if (selectedSubject is null)
            {
                foreach (var VARIABLE in Rat)
                {
                    
                }
            }

            if (selectedSubject is null || (GetMark(rating.PointsFirstModule, rating.PointsSecondModule) == 4 &&
                                            rating.SubjectName == selectedSubject))
                tuple.Add((student.Name, student.Group, rating.PointsFirstModule));
        }

        var viewModel = new Task1ViewModel
            { Tuple = tuple, Subjects = _availableSubjects, SubjectString = selectedSubject ?? "Усі предмети" };
        return View(viewModel);
    }


    [HttpPost]
    public IActionResult Index(Task1ViewModel task1ViewModel)
    {
        if (!ModelState.IsValid)
        {
            RedirectToAction("Index");
        }

        return Index(task1ViewModel.SubjectString);
    }
}