using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Diagnostics;
using static Airlines.Program;

namespace Airlines.UnitTest
{
    public class SortingTests
    {
        [Fact]
        public void AirportBoubleSortShouldWork()
        {
            string[] array = {"aaa", "ccc", "bbb" };
            string[] result = { "aaa", "bbb", "ccc" };

            string[] sorted = AirportsSorting(array);

            Assert.Equal(sorted, result);
        }

        [Fact]
        public void AirlineSelectionSortShouldWork() 
        {
            string[] array = { "aaa", "ccc", "bbb" };
            string[] result = { "aaa", "bbb", "ccc" };

            string[] sorted = AirlinesSorting(array);

            Assert.Equal(sorted, result);
        }

        [Fact]
        public void FlightSelectionSortShouldWork()
        {
            string[] array = { "aaa", "ccc", "bbb" };
            string[] result = { "aaa", "bbb", "ccc" };

            string[] sorted = FlightSorting(array);

            Assert.Equal(sorted, result);
        }

        static string[] GenerateRandomStringArray(int length, int stringLength)
        {
            string[] randomStrings = new string[length];
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for (int i = 0; i < length; i++)
            {
                char[] stringChars = new char[stringLength];

                for (int j = 0; j < stringLength; j++)
                {
                    stringChars[j] = chars[random.Next(chars.Length)];
                }

                randomStrings[i] = new string(stringChars);
            }

            return randomStrings;
        }

        [Fact]
        public void SortingPerformanceTest()
        {

            var array = GenerateRandomStringArray(4, 5);

            int counter1 = 0;
            int counter2 = 0;

            var watch = new Stopwatch();

            watch.Start();

            while (watch.ElapsedMilliseconds < 1000)
            {
                string[] boubleSort = AirportsSorting(array);
                counter1++;
            }

            watch.Stop();
            watch.Reset();
            watch.Start();

            while (watch.ElapsedMilliseconds < 1000)
            {
                ;
                string[] selectionSort = FlightSorting(array);
                counter2++;
            }
            Console.WriteLine(counter1);
            Console.WriteLine(counter2);

        }
    }
}
