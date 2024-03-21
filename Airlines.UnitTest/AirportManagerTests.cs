using Airlines.Business;
using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Airlines.Console.Tests
{
    public class AirportManagerTests
    {
        [Fact]
        public void Add_Airport_Successfully()
        {
            var airportManager = new AirportManager();
            var airport = new Airport
            {
                Id = "ABC",
                Name = "Test Airport",
                City = "Test City",
                Country = "Test Country"
            };

            airportManager.Add(airport);

            Assert.Contains(airport.Id, airportManager.Airports.Keys);
            Assert.Contains(airport, airportManager.Airports.Values);
            Assert.Contains(airport, airportManager.AirportsByCity["Test City"]);
            Assert.Contains(airport, airportManager.AirportsByCountry["Test Country"]);
            Assert.Contains(airport.Name, airportManager.AirportNames);
        }

        [Fact]
        public void Add_Duplicate_Airport_Fails()
        {
            var airportManager = new AirportManager();
            var airport = new Airport
            {
                Id = "ABC",
                Name = "Test Airport",
                City = "Test City",
                Country = "Test Country"
            };

            airportManager.Add(airport); // Adding the same airport once
            airportManager.Add(airport); // Adding the same airport again

            Assert.Single(airportManager.Airports); // Only one airport should be added
        }

        [Fact]
        public void Search_Airport_By_Name()
        {
            var airportManager = new AirportManager();
            var airport = new Airport
            {
                Id = "ABC",
                Name = "Test Airport",
                City = "Test City",
                Country = "Test Country"
            };
            airportManager.Add(airport);

            var writer = new StringWriter();
            System.Console.SetOut(writer);

            airportManager.Search("Test Airport");

            var output = writer.ToString().Trim();
            Assert.Contains("Test Airport", output);
        }

        [Theory]
        [InlineData("Test Airport", true)]
        [InlineData("Nonexistent Airport", false)]
        public void Check_Airport_Existence(string airportName, bool expectedResult)
        {
            var airportManager = new AirportManager();
            var airport = new Airport
            {
                Id = "ABC",
                Name = "Test Airport",
                City = "Test City",
                Country = "Test Country"
            };
            airportManager.Add(airport);

            var result = airportManager.Exist(airportName);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void List_Airports_By_City()
        {
            var airportManager = new AirportManager();
            var airport1 = new Airport
            {
                Id = "ABC",
                Name = "Test Airport 1",
                City = "Test City",
                Country = "Test Country"
            };
            var airport2 = new Airport
            {
                Id = "DEF",
                Name = "Test Airport 2",
                City = "Test City",
                Country = "Test Country"
            };
            airportManager.Add(airport1);
            airportManager.Add(airport2);

            var writer = new StringWriter();
            System.Console.SetOut(writer);

            airportManager.ListData("Test City", "City");

            var output = writer.ToString().Trim();
            Assert.Contains("Test Airport 1", output);
            Assert.Contains("Test Airport 2", output);
        }

        [Fact]
        public void List_Airports_By_Country()
        {
            var airportManager = new AirportManager();
            var airport1 = new Airport
            {
                Id = "ABC",
                Name = "Test Airport 1",
                City = "Test City",
                Country = "Test Country"
            };
            var airport2 = new Airport
            {
                Id = "DEF",
                Name = "Test Airport 2",
                City = "Test City",
                Country = "Test Country"
            };
            airportManager.Add(airport1);
            airportManager.Add(airport2);

            var writer = new StringWriter();
            System.Console.SetOut(writer);

            airportManager.ListData("Test Country", "Country");

            var output = writer.ToString().Trim();
            Assert.Contains("Test Airport 1", output);
            Assert.Contains("Test Airport 2", output);
        }
    }
}
