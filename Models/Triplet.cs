using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab3.Models;

public class Triplet
{
    public List<(string Name, string Group)>? Pairs { get; set; }
    public List<SelectListItem>? Marks { get; set; }
    public string? MarkString { get; set; }
}