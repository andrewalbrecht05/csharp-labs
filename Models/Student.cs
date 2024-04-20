namespace Lab3.Models;

public class Student: IParseable<Student>
{
    public int Id { get; set; }
    public string Name { get; private init; } = "";
    public string? Group { get; set; }

    public static Student Parse(string line, string separator = ";")
    {
        var words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (words.Length != 3 || !int.TryParse(words[0], out var id))
            throw new FormatException($"Line {line} cannot be parsed as valid instance of Student class");
        return new Student { Id = id, Name = words[1], Group = words[2] };
    }

    public override string ToString() => $"{Id}, {Name} {Group}";
}