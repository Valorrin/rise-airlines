using static Airlines.Program;

namespace Airlines.UnitTests
{
    public class DataManipulationTests
    {
        [Fact]
        public void AddData_ShouldAddItemToEmptyArray()
        {
            string item = "abc";
            string[] data = new string[3];

            var result = AddData(item, data);

            Assert.Equal(new string[] { "abc", null, null }, result);
        }

        [Fact]
        public void AddData_ShouldAddItemToNonEmptyArray()
        {
            string item = "def";
            string[] data = ["abc", "xyz", null];

            var result = AddData(item, data);

            Assert.Equal(new string[] { "abc", "xyz", "def" }, result);
        }
    }
}
