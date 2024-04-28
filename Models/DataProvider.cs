using System.Diagnostics;

namespace Lab3.Models;

public static class DataProvider
{
    private static List<Student>? _students;
    private static List<Rating>? _ratings;
    
    private static Dictionary<int, Student>? _studentsDict;
    private static Dictionary<(int,string), Rating>? _ratingsDict;

    public const string DefaultDir = "./AppData";
    private static string _studentsFileName = "Students.csv";
    private static string _ratingsFileName = "Ratings.csv";

    public static List<Student> Students
    {
        get { return _students ??= ReadFromFile<Student>(_studentsFileName); }
    }

    public static Dictionary<int, Student> StudentsDict => _studentsDict ??= Students.ToDictionary(d => d.Id, d => d);

    public static List<Rating> Ratings => _ratings ??= ReadFromFile<Rating>(_ratingsFileName);

    public static Dictionary<(int,string), Rating> RatingsDict => _ratingsDict ??= Ratings.ToDictionary(p => (p.Id,p.SubjectName), p => p);

    public static List<T> ReadFromFile<T>(string fileName, string separator = ";") where T : IParseable<T>
    {
        var elements = new List<T>();
        int lineNumber = 0;
        var fullFileName = Path.Combine(DefaultDir, fileName);
        Trace.WriteLine($"{DateTime.Now:HH:mm:ss}: {fullFileName} load started");
        try
        {
            using var reader = new StreamReader(fullFileName);
            while (!reader.EndOfStream)
            {
                lineNumber++;
                var line = reader.ReadLine();
                if (line is null || line.StartsWith('#'))
                    continue;
                try
                {
                    elements.Add(T.Parse(line, separator));
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error occurred during parsing {line}");
                    Trace.WriteLine($"{fullFileName}: inconsistent data in line #{lineNumber}");
                }
            }
        }
        catch (Exception e)
        {
            Trace.WriteLine($"{fullFileName}: exception {e.Message}");
        }
        finally
        {
            Trace.WriteLine($"{DateTime.Now:HH:mm:ss}: {fullFileName} load finished");
        }

        return elements;
    }
}