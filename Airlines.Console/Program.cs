using System;

namespace Airlines
{
    internal class Program
    {
        static void Main()
        {
            var airports = Array.Empty<string>();
            var airlines = Array.Empty<string>();
            var flights = Array.Empty<string>();

            ReadInput("Airport", ref airports);
            ReadInput("Airline", ref airlines);
            ReadInput("Flight", ref flights);

            PrintData("Airport", airports);
            PrintData("Airlines", airlines);
            PrintData("Flights", flights);
        }

        static void ReadInput(string type, ref string[] data)
        {
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
                    if (ValidateAirport(input))
                    {
                        data = AddData(input, data);
                    }
                }
                else if (type == "Airline")
                {
                    if (ValidateAirline(input))
                    {
                        data = AddData(input, data);
                    }
                }
                else if (type == "Flight")
                {

                    if (ValidateFlight(input))
                    {
                        data = AddData(input, data);
                    }
                }
            }
        }

        static bool ValidateAirport(string airport)
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

            return true;
        }

        static bool ValidateAirline(string airline)
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

            return true;
        }

        static bool ValidateFlight(string flight)
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
    }
}