using System;
using System.Diagnostics.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Airlines
{
    internal class Program
    {
        static void Main()
        {
            var airports = Array.Empty<string>();
            var airlines = Array.Empty<string>();
            var flights = Array.Empty<string>();

            airports = ReadInput("Airport", airports);
            airlines = ReadInput("Airline", airlines);
            flights = ReadInput("Flight", flights);

            PrintData("Airport", airports);
            PrintData("Airlines", airlines);
            PrintData("Flights", flights);

            AirportsSorting(airports);
            AirlinesSorting(airlines);
            FlightSorting(flights);

            PrintData("Airport", airports);
            PrintData("Airlines", airlines);
            PrintData("Flights", flights);

            Search(airports, airlines, flights);
        }

        static string[] ReadInput(string type, string[] currentData)
        {
            var newData = currentData;

            Console.WriteLine($"Enter {type} name, or type 'done' to finish:\n");

            while (true)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "done")
                {
                    Console.WriteLine();
                    break;
                }

                if (type == "Airport")
                {
                    if (ValidateAirport(input, newData))
                    {
                        newData = AddData(input, newData);
                    }
                }
                else if (type == "Airline")
                {
                    if (ValidateAirline(input, newData))
                    {
                        newData = AddData(input, newData);
                    }
                }
                else if (type == "Flight")
                {

                    if (ValidateFlight(input, newData))
                    {
                        newData = AddData(input, newData);
                    }
                }
            }

            return newData;
        }

        static bool ValidateAirport(string airport, string[] airports)
        {
            if (string.IsNullOrEmpty(airport))
            {
                Console.WriteLine(" Error: Airport name cannot be null or empty!");
                return false;
            }

            if (airport.Length != 3)
            {
                Console.WriteLine($" Error: Airport name '{airport}' must be exactly 3 characters long!");
                return false;
            }

            if (!airport.All(char.IsLetter))
            {
                Console.WriteLine($" Error: Airport name '{airport}' must contain only alphabetic characters!");
                return false;
            }

            if (airports.Contains(airport))
            {
                Console.WriteLine($" Error: Airport name '{airport}' already exist!");
                return false;
            }

            return true;
        }

        static bool ValidateAirline(string airline, string[] airlines)
        {
            if (string.IsNullOrEmpty(airline))
            {
                Console.WriteLine(" Error: Airline name cannot be null or empty!");
                return false;
            }

            if (airline.Length >= 6)
            {
                Console.WriteLine($" Error: Airline name '{airline}' must be less than 6 characters long!");
                return false;
            }

            if (airlines.Contains(airline))
            {
                Console.WriteLine($" Error: Airline name '{airline}' already exist!");
                return false;
            }

            return true;
        }

        static bool ValidateFlight(string flight, string[] flights)
        {
            if (string.IsNullOrEmpty(flight))
            {
                Console.WriteLine(" Error: Flight name cannot be null or empty!");
                return false;
            }

            if (!flight.All(char.IsLetterOrDigit))
            {
                Console.WriteLine($" Error: Flight code '{flight}' must contain only alphabetic or numeric characters!");
                return false;
            }

            if (flights.Contains(flight))
            {
                Console.WriteLine($" Error: Flight name '{flight}' already exist!");
                return false;
            }

            return true;
        }

        static string[] AddData(string item, string[] oldData)
        {
            var updatedData = new string[oldData.Length + 1];

            for (int i = 0; i < oldData.Length; i++)
            {
                updatedData[i] = oldData[i];
            }
            updatedData[updatedData.Length - 1] = item;

            Console.WriteLine($" {item} was added successfully!");

            return updatedData;
        }

        static void PrintData(string type, string[] data)
        {
            Console.Write($" {type}s: ");
            if (data.Length == 0)
            {
                Console.WriteLine("none");
            }
            else
            {
                Console.WriteLine(string.Join(", ", data));
            }
        }

        static string[] AirportsSorting(string[] airports)
        {
            var n = airports.Length;
            string temp;

            for (int j = 0; j < n - 1; j++)
            {
                for (int i = j + 1; i < n; i++)
                {
                    if (airports[j].CompareTo(airports[i]) > 0)
                    {
                        temp = airports[j];
                        airports[j] = airports[i];
                        airports[i] = temp;
                    }
                }
            }

            return airports;
        }

        static string[] AirlinesSorting(string[] airlines)
        {
            int n = airlines.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (string.Compare(airlines[j], airlines[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    string temp = airlines[i];
                    airlines[i] = airlines[minIndex];
                    airlines[minIndex] = temp;
                }
            }

            return airlines;
        }

        static string[] FlightSorting(string[] flights)
        {
            int n = flights.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (string.Compare(flights[j], flights[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    string temp = flights[i];
                    flights[i] = flights[minIndex];
                    flights[minIndex] = temp;
                }
            }

            return flights;
        }

        static void Search(string[] airports, string[] airlines, string[] flights)
        {
            AirportsSorting(airports);
            AirlinesSorting(airlines);
            AirlinesSorting(flights);

            Console.WriteLine($"\nEnter search term or type 'done' to finish:\n");

            while (true)
            {
                string searchTerm = Console.ReadLine();

                bool termFound = false;

                if (string.IsNullOrEmpty(searchTerm))
                {
                    Console.WriteLine(" Error: search term cannot be null or empty!");
                    continue;
                }

                if (searchTerm.ToLower() == "done")
                {
                    Console.WriteLine();
                    break;
                }

                if (Array.BinarySearch(airports, searchTerm) >= 0)
                {
                    termFound = true;
                    Console.WriteLine($" {searchTerm} is Airport name.");
                }

                if (Array.BinarySearch(airlines, searchTerm) >= 0)
                {
                    termFound = true;
                    Console.WriteLine($" {searchTerm} is Airline name.");
                }

                if (Array.BinarySearch(flights, searchTerm) >= 0)
                {
                    termFound = true;
                    Console.WriteLine($" {searchTerm} is Flight name.");
                }

                if (!termFound)
                {
                    Console.WriteLine($" {searchTerm} was not found.");
                }
            }
        }
    }
}