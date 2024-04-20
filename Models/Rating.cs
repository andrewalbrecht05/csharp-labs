namespace Lab3.Models;

public record Rating(int Id, string Name, int pointsFirstModule, int pointsSecondModule ): IParseable<Rating>
{
    public static Rating Parse(string line, string separator = ";")
    {
        var words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (words.Length != 4 || !int.TryParse(words[0], out var id) || !int.TryParse(words[2], out var pointsFirstModule) 
            || !int.TryParse(words[3], out var pointsSecondModule ))
            throw new FormatException($"Line {line} cannot be parsed as valid instance of Rating class");
        return new Rating(id, words[1], pointsFirstModule, pointsSecondModule);
    }
}