using static Airlines.Console.InputReader;

namespace Airlines.UnitTests;
public class InputReaderTests
{
    [Fact]
    public void ReadFromConsole_WhenInputIsNotNullOrEmpty_ReturnsInput()
    {
        var expectedInput = "Flight123";
        var inputStream = new StringReader(expectedInput);
        System.Console.SetIn(inputStream);

        var actualInput = ReadFromConsole();

        Assert.Equal(expectedInput, actualInput);
    }

    [Fact]
    public void ReadFromFile_ShouldReadLinesFromFile()
    {
        var filePath = "testfile.txt";
        var testData = new List<string> { "Line1", "Line2", "Line3" };

        File.WriteAllLines(filePath, testData);

        var result = ReadFromFile(filePath);

        Assert.Equal(testData, result);

        File.Delete(filePath);
    }

    [Fact]
    public void ReadCommands_WhenInputIsNotNullOrEmpty_ReturnsInput()
    {
        var expectedInput = "Command123";
        var inputStream = new StringReader(expectedInput);
        System.Console.SetIn(inputStream);

        var actualInput = ReadCommands();

        Assert.Equal(expectedInput, actualInput);
    }
}