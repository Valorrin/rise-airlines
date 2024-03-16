using static Airlines.Program;

namespace Airlines.UnitTests
{
    public class DataManipulationTests
    {
        [Fact]
        public void AddData_ItemAddedSuccessfully()
        {
            string itemToAdd = "New Item";
            string[] oldData = { "Item1", "Item2", "Item3" };

            string[] updatedData = AddData(itemToAdd, oldData);

            Assert.Equal(oldData.Length + 1, updatedData.Length);
            Assert.Equal(itemToAdd, updatedData[updatedData.Length - 1]);
        }
    }
}
