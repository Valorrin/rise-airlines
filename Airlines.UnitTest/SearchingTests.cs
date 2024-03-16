using static Airlines.Program;

namespace Airlines.UnitTest
{
    public class SearchingTests
    {
        [Fact]
        public void BinarySearch_ShouldFindTarget()
        {
            string[] array = ["a", "b", "c", "d"];
            string target = "c";

            int result = BinarySearch(array, target);

            Assert.NotEqual(-1 , result);
        }

        [Fact]
        public void BinarySearch_ShouldNotFindTarget()
        {
            string[] array = ["a", "b", "c", "d"];
            string target = "e";

            int result = BinarySearch(array, target);

            Assert.Equal(-1, result);
        }

        [Fact]
        public void LinearSearch_ShouldFindTarget()
        {
            string[] array = ["a", "b", "c", "d"];
            string target = "c";

            int result = LinearSearch(array, target);

            Assert.NotEqual(-1, result);
        }

        [Fact]
        public void LinearSearch_ShouldNotFindTarget()
        {
            string[] array = ["a", "b", "c", "d"];
            string target = "e";

            int result = LinearSearch(array, target);

            Assert.Equal(-1, result);
        }
    }
}
