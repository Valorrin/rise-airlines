using System;
using System.Collections.Generic;
using Xunit;

namespace Airlines.Console.Tests
{
    public class SortExtensionsTests
    {
        [Fact]
        public void Sort_AirportManager_Sorts_In_Ascending_Order()
        {
            var manager = new AirportManager();
            manager.Airports.Add("Zur");
            manager.Airports.Add("Par");
            manager.Airports.Add("Lon");
            manager.Airports.Add("New");
            var expected = new List<string> { "Lon", "New", "Par", "Zur" };

            manager.Sort();

            Assert.Equal(expected, manager.Airports);
        }

        [Fact]
        public void Sort_AirlineManager_Sorts_In_Ascending_Order()
        {
            var manager = new AirlineManager();
            manager.Airlines.Add("Delta");
            manager.Airlines.Add("British Airways");
            manager.Airlines.Add("American Airlines");
            manager.Airlines.Add("Lufthansa");
            var expected = new List<string> { "American Airlines", "British Airways", "Delta", "Lufthansa" };

            manager.Sort();

            Assert.Equal(expected, manager.Airlines);
        }

        [Fact]
        public void Sort_FlightManager_Sorts_In_Ascending_Order()
        {
            var manager = new FlightManager();
            manager.Flights.Add("FL123");
            manager.Flights.Add("FL456");
            manager.Flights.Add("FL789");
            manager.Flights.Add("FL101");
            var expected = new List<string> { "FL101", "FL123", "FL456", "FL789" };

            manager.Sort();

            Assert.Equal(expected, manager.Flights);
        }

        [Fact]
        public void SortDesc_AirportManager_Sorts_In_Descending_Order()
        {
            var manager = new AirportManager();
            manager.Airports.Add("Zurich");
            manager.Airports.Add("Paris");
            manager.Airports.Add("London");
            manager.Airports.Add("New York");
            var expected = new List<string> { "Zurich", "Paris", "New York", "London" };

            manager.SortDesc();

            Assert.Equal(expected, manager.Airports);
        }

        [Fact]
        public void SortDesc_AirlineManager_Sorts_In_Descending_Order()
        {
            var manager = new AirlineManager();
            manager.Airlines.Add("Delta");
            manager.Airlines.Add("British Airways");
            manager.Airlines.Add("American Airlines");
            manager.Airlines.Add("Lufthansa");
            var expected = new List<string> { "Lufthansa", "Delta", "British Airways", "American Airlines" };

            manager.SortDesc();

            Assert.Equal(expected, manager.Airlines);
        }

        [Fact]
        public void SortDesc_FlightManager_Sorts_In_Descending_Order()
        {
            var manager = new FlightManager();
            manager.Flights.Add("FL123");
            manager.Flights.Add("FL456");
            manager.Flights.Add("FL789");
            manager.Flights.Add("FL101");
            var expected = new List<string> { "FL789", "FL456", "FL123", "FL101" };

            manager.SortDesc();

            Assert.Equal(expected, manager.Flights);
        }
    }
}
