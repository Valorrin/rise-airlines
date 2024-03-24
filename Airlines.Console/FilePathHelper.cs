using System.Reflection;

namespace Airlines.Console;
public class FilePathHelper
{
    public static string GetFilePath(string fileName)
    {
        var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var projectDirectory = Directory.GetParent(currentDirectory!)?.Parent?.Parent?.FullName;

        return projectDirectory != null
            ? Path.Combine(projectDirectory, "Data", fileName)
            : throw new InvalidOperationException("Unable to determine project directory.");
    }
}