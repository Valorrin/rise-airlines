using Airlines.Console.Utilities;

namespace Airlines.UnitTests.UtilityTests;


[Collection("Sequential")]
public class FilePathHelperTests
{
    [Fact]
    public void GetFilePath_ValidFileName_ReturnsCorrectPath()
    {
        var fileName = "example.txt";

        var filePath = FilePathHelper.GetFilePath(fileName);

        Assert.EndsWith($"Data{Path.DirectorySeparatorChar}{fileName}", filePath);
        Assert.Contains("Data", filePath);
    }

    [Fact]
    public void GetFilePath_NullFileName_ThrowsArgumentNullException()
    {
        string? fileName = null;

        _ = Assert.Throws<ArgumentNullException>(() => FilePathHelper.GetFilePath(fileName!));
    }
}
