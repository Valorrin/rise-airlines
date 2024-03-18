using System.Diagnostics;
using Xunit.Abstractions;
using static Airlines.Program;

namespace Airlines.UnitTests;

public class AlgorithmsPerformanceTests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;
    private static readonly Random _random = new(1234);


    public static string[] GenerateRandomStrings(int count)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var randomStrings = new string[count];

        for (var i = 0; i < count; i++)
        {
            var length = _random.Next(1, 100);

            randomStrings[i] = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        return randomStrings;
    }

    [Fact]
    public void BubbleSort_PerformanceTest()
    {
        var data = GenerateRandomStrings(1000);

        Stopwatch stopwatch = new();
        var iterations = 0;

        stopwatch.Start();

        while (stopwatch.ElapsedMilliseconds < 1000)
        {
            _ = SortAirlines(data);
            iterations++;
        }

        stopwatch.Stop();

        _output.WriteLine($"Bubble iterations: {iterations}");
        Assert.True(iterations > 0);
    }

    [Fact]
    public void SelectionSort_PerformanceTest()
    {
        var data = GenerateRandomStrings(1000);

        Stopwatch stopwatch = new();
        var iterations = 0;

        stopwatch.Start();

        while (stopwatch.ElapsedMilliseconds < 1000)
        {
            _ = SortAirlines(data);
            iterations++;
        }

        stopwatch.Stop();

        _output.WriteLine($"Selection iterations: {iterations}");
        Assert.True(iterations > 0);
    }

    [Fact]
    public void LinearSearch_PerformanceTest()
    {
        var data = GenerateRandomStrings(10000);

        Random random = new();

        Stopwatch stopwatch = new();
        var iterations = 0;

        stopwatch.Start();

        while (stopwatch.ElapsedMilliseconds < 1000)
        {
            var randomIndex = random.Next(0, data.Length);
            var target = data[randomIndex];

            _ = LinearSearch(data, target);
            iterations++;
        }

        stopwatch.Stop();

        _output.WriteLine($"Linear iterations: {iterations}");
        Assert.True(iterations > 0);
    }

    [Fact]
    public void BinarySearch_PerformanceTest()
    {
        var data = GenerateRandomStrings(10000);

        Random random = new();

        Stopwatch stopwatch = new();
        var iterations = 0;

        stopwatch.Start();

        while (stopwatch.ElapsedMilliseconds < 1000)
        {
            var randomIndex = random.Next(0, data.Length);
            var target = data[randomIndex];

            _ = BinarySearch(data, target);
            iterations++;
        }

        stopwatch.Stop();

        _output.WriteLine($"Binary iterations: {iterations}");
        Assert.True(iterations > 0);
    }
}
