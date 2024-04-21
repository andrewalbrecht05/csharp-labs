using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab3.Models;

public class Task1ViewModel
{
    public List<(string Name, string? Group, int? Mark)>? Tuple { get; set; }
    public List<SelectListItem>? Marks { get; set; }
    public string? MarkString { get; set; }
}