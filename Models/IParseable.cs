namespace Lab3.Models;

public interface IParseable<out T>
{
    public static abstract T Parse(string line, string separator = ";");
}