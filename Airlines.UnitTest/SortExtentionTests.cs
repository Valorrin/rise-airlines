using Airlines.Business;
using Xunit;
using System;
using System.Collections.Generic;

namespace Airlines.Console.Tests
{
    public class SortExtensionsTests
    {
        [Fact]
        public void Sort_FlightManager_Sorts_In_Ascending_Order()
        {
            var manager = new FlightManager();
            manager.Add("FL123");
            manager.Add("FL456");
            manager.Add("FL789");
            manager.Add("FL101");
            var expected = new List<string> { "FL101", "FL123", "FL456", "FL789" };

            manager.Sort();

            Assert.Equal(expected, manager.Flights);
        }

        [Fact]
        public void SortDesc_FlightManager_Sorts_In_Descending_Order()
        {
            var manager = new FlightManager();
            manager.Add("FL123");
            manager.Add("FL456");
            manager.Add("FL789");
            manager.Add("FL101");
            var expected = new List<string> { "FL789", "FL456", "FL123", "FL101" };

            manager.SortDesc();

            Assert.Equal(expected, manager.Flights);
        }
    }
}
