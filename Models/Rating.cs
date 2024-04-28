namespace Lab3.Models;

public record Rating(int Id, string StudentName, string SubjectName, int PointsFirstModule, int PointsSecondModule ): IParseable<Rating>
{
    public static Rating Parse(string line, string separator = ";")
    {
        var words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (words.Length != 5 || !int.TryParse(words[0], out var id) || !int.TryParse(words[3], out var pointsFirstModule) 
            || !int.TryParse(words[4], out var pointsSecondModule ))
            throw new FormatException($"Line {line} cannot be parsed as valid instance of Rating class");
        return new Rating(id, words[1],  words[2], pointsFirstModule, pointsSecondModule);
    }
}