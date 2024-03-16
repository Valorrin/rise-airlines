using System.Diagnostics;
using Xunit.Abstractions;
using static Airlines.Program;

namespace Airlines.UnitTests
{
    public class AlgorithmsPerformanceTests(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper output = output;
        private static readonly Random random = new(1234);


        public static string[] GenerateRandomStrings(int count)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string[] randomStrings = new string[count];

            for (int i = 0; i < count; i++)
            {
                int length = random.Next(1, 100); 

                randomStrings[i] = new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            return randomStrings;
        }

        [Fact]
        public void BubbleSort_PerformanceTest()
        {
            string[] data = GenerateRandomStrings(1000);

            Stopwatch stopwatch = new();
            int iterations = 0;

            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < 1000)
            {
                SortAirlines(data);
                iterations++;
            }

            stopwatch.Stop();

            output.WriteLine($"Bubble iterations: {iterations}");
            Assert.True(iterations > 0);
        }

        [Fact]
        public void SelectionSort_PerformanceTest()
        {
            string[] data = GenerateRandomStrings(1000);

            Stopwatch stopwatch = new();
            int iterations = 0;

            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < 1000)
            {
                SortAirlines(data);
                iterations++;
            }

            stopwatch.Stop();

            output.WriteLine($"Selection iterations: {iterations}");
            Assert.True(iterations > 0);
        }

        [Fact]
        public void LinearSearch_PerformanceTest()
        {
            string[] data = GenerateRandomStrings(10000);

            Random random = new();

            Stopwatch stopwatch = new();
            int iterations = 0;

            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < 1000) 
            {
                int randomIndex = random.Next(0, data.Length);
                string target = data[randomIndex];

                LinearSearch(data, target);
                iterations++;
            }

            stopwatch.Stop();

            output.WriteLine($"Linear iterations: {iterations}");
            Assert.True(iterations > 0);
        }

        [Fact]
        public void BinarySearch_PerformanceTest()
        {
            string[] data = GenerateRandomStrings(10000);

            Random random = new();

            Stopwatch stopwatch = new();
            int iterations = 0;

            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < 1000)
            {
                int randomIndex = random.Next(0, data.Length);
                string target = data[randomIndex];

                BinarySearch(data, target);
                iterations++;
            }

            stopwatch.Stop();

            output.WriteLine($"Binary iterations: {iterations}");
            Assert.True(iterations > 0);
        }


    }
}
