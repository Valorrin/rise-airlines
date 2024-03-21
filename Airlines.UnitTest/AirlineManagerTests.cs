﻿using Airlines.Business;
using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Airlines.Console.Tests
{
    public class AirlineManagerTests
    {
        [Fact]
        public void Add_Airline_Successfully()
        {
            var airlineManager = new AirlineManager();
            var airline = new Airline
            {
                Id = "ABC",
                Name = "Test Airline"
            };

            airlineManager.Add(airline);

            Assert.Contains(airline.Id, airlineManager.Airlines.Keys);
            Assert.Contains(airline, airlineManager.Airlines.Values);
        }

        [Fact]
        public void Add_Duplicate_Airline_Fails()
        {
            var airlineManager = new AirlineManager();
            var airline = new Airline
            {
                Id = "ABC",
                Name = "Test Airline"
            };

            airlineManager.Add(airline);

            _ = Assert.Throws<ArgumentException>(() => airlineManager.Add(airline));

            _ = Assert.Single(airlineManager.Airlines);
        }

        [Theory]
        [InlineData("Test Airline", true)]
        [InlineData("Nonexistent Airline", false)]
        public void Search_Airline_By_Name(string airlineName, bool expectedResult)
        {
            var airlineManager = new AirlineManager();
            var airline = new Airline
            {
                Id = "ABC",
                Name = "Test Airline"
            };
            airlineManager.Add(airline);

            var writer = new StringWriter();
            System.Console.SetOut(writer);

            airlineManager.Search(airlineName);

            var output = writer.ToString().Trim();
            Assert.Equal(expectedResult, output.Contains(airlineName));
        }

        [Fact]
        public void IsIdUnique_Returns_True_When_Id_Is_Unique()
        {
            var airlineManager = new AirlineManager();
            var id = "ABC";

            var result = airlineManager.IsIdUnique(id);

            Assert.True(result);
        }

        [Fact]
        public void IsIdUnique_Returns_False_When_Id_Is_Not_Unique()
        {
            var airlineManager = new AirlineManager();
            var airline = new Airline
            {
                Id = "ABC",
                Name = "Test Airline"
            };
            airlineManager.Add(airline);
            var id = "ABC";

            var result = airlineManager.IsIdUnique(id);

            Assert.False(result);
        }
    }
}
