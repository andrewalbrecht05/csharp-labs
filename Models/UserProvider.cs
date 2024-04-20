using System.Diagnostics;

namespace Lab3.Models;

public class UserProvider
{
    private const string DefaultFileName = "users.txt";
    private const string DefaultDataDir = "./AppData";
    private static string _fullName = string.Empty;
    private static readonly object Lock = new();

    private static readonly Dictionary<string, User> Users = new();

    static UserProvider()
    {
        ReadUsers();
    }

    public static void ClearUsers() => Users.Clear();

    public static bool IsAuthorizedUser(string username, string password) =>
        Users.ContainsKey(username) && Users[username].Password == password;

    public static bool HasAccount(string userName) => Users.ContainsKey(userName);

    public static void ReadUsers(string? fileName = null, bool append = true)
    {
        fileName ??= DefaultFileName;
        _fullName = Path.Combine(DataProvider.DefaultDir, fileName);
        var userList = DataProvider.ReadFromFile<User>(fileName, separator: "\t");
        if (!append)
        {
            ClearUsers();
            Trace.WriteLine($"{DateTime.Now.TimeOfDay}: user list cleared");
        }

        // Bug? Next indent does not work
        //Trace.Indent();
        int count = 0;
        lock (Lock)
        {
            for (var index = 0; index < userList.Count; index++)
            {
                if (!Users.TryAdd(userList[index].Name, userList[index]))
                {
                    Trace.WriteLine(
                        $"{fileName}: repeated occurrence of {userList[index].Name} is ignored");
                }
                else
                {
                    count++;
                }
            }
        }

        //Trace.Unindent();
        Trace.WriteLine($"{DateTime.Now:HH:mm:ss}: {count} users loaded");
    }

    public static bool TryAddUser(User user)
    {
        var result = false;
        if (!Users.ContainsKey(user.Name))
        {
            try
            {
                lock (Lock)
                {
                    using var writer = new StreamWriter(_fullName, append: true);
                    Users.Add(user.Name, user);
                    writer.Write($"\n{user}");
                }

                result = true;
                Trace.WriteLine($"{DateTime.Now:HH:mm:ss}: {user.Name} is saved in {_fullName}");
            }
            catch (IOException e)
            {
                Trace.WriteLine(
                    $"{DateTime.Now.TimeOfDay}: {user.Name} saving in {_fullName} failed with exception: {e.Message}");
            }
        }

        return result;
    }

    public static bool TrySaveUsers(string? dataDir = null, string? fileName = null)
    {
        dataDir ??= DefaultDataDir;
        fileName ??= DefaultFileName;
        var fullName = Path.Combine(dataDir, fileName);
        bool result = false;
        try
        {
            lock (Lock)
            {
                Trace.WriteLine($"{DateTime.Now:HH:mm:ss}: users save started in {fullName}");
                using var writer = new StreamWriter(fullName, append: true);
                foreach (var user in Users)
                {
                    writer.WriteLine(user);
                }
            }

            result = true;
            Trace.WriteLine($"{DateTime.Now:HH:mm:ss}: users save finished. {Users.Count} users saved in {fullName}");
        }
        catch (Exception e)
        {
            Trace.WriteLine($"Users saving in {fullName} failed: exception {e.Message}");
        }

        return result;
    }
}