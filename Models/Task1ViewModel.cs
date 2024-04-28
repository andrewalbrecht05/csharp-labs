using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab3.Models;

public class Task1ViewModel
{
    public List<(string Name, string? Group, int? mark)>? Tuple { get; set; }
    public List<SelectListItem>? Subjects { get; set; }
    public string? SubjectString { get; set; }
}