using System;

namespace Airlines
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var flights = Array.Empty<string>();
            var airlines = Array.Empty<string>();
            var airports = Array.Empty<string>();

            while (true)
            {
                string airport = Console.ReadLine();

                if (ValidateAirport(airport))
                {
                    airports = AddAirport(airport, airports);
                }

                PrintAirports(airports);
            }

            while (false)
            {
                string airline = Console.ReadLine();
                if (ValidateAirline(airline))
                {
                    airlines = AddAirline(airline, airlines);
                }

                PrintAirlines(airlines);
            }

            while (false)
            {
                string flight = Console.ReadLine();
                if (ValidateFlight(flight))
                {
                    flights = AddFlight(flight, flights);
                }

                PrintFlights(flights);
            }
        }
        
        static bool ValidateAirport(string airport)
        {
            if (airport == null)
            {
                Console.WriteLine("The airport name is null!");
                return false;
            }

            if (airport.Length != 3)
            {
                Console.WriteLine($"The airport name {airport} must be 3 symbols!");
                return false;
            }


            char[] charArray = airport.ToCharArray();

            for (int i = 0; i < charArray.Length; i++) 
            {
                if (!Char.IsLetter(charArray[i]))
                {
                    Console.WriteLine($"{charArray[i]} is not alphabetic!");
                    return false;
                }
            }
            return true;
        }

        static bool ValidateAirline(string airline) 
        {
            if (airline == null)
            {
                Console.WriteLine("The airline name is null!");
                return false;
            }

            if (airline.Length >= 6)
            {
                Console.WriteLine($"The airline name {airline} must be less than 6 symbols!");
                return false;
            }
            return true;
        }

        static bool ValidateFlight(string flight)
        {
            if (flight == null)
            {
                Console.WriteLine("The flight name is null!");
                return false;
            }

            char[] charArray = flight.ToCharArray();

            for (int i = 0; i < charArray.Length; i++)
            {
                if (!Char.IsLetterOrDigit(charArray[i]))
                {
                    Console.WriteLine($"{charArray[i]} is not alphabetic or numeric!");
                    return false;
                }
            }
            return true;
        }

        static string[] AddAirport(string airport, string[] oldAirports)
        {
            var updatedAirports = new string[oldAirports.Length + 1];

            for (int i = 0; i < oldAirports.Length; i++)
            {
                updatedAirports[i] = oldAirports[i];
            }
            updatedAirports[updatedAirports.Length - 1] = airport;

            return updatedAirports;
        }

        static string[] AddAirline(string airline, string[] oldAirlines)
        {
            var updatedAirlines = new string[oldAirlines.Length + 1];

            for (int i = 0; i < oldAirlines.Length; i++)
            {
                updatedAirlines[i] = oldAirlines[i];
            }
            updatedAirlines[updatedAirlines.Length - 1] = airline;

            return updatedAirlines;
        }

        static string[] AddFlight(string flight, string[] oldFlights)
        {
            var updatedFlights = new string[oldFlights.Length + 1];

            for (int i = 0; i < oldFlights.Length; i++)
            {
                updatedFlights[i] = oldFlights[i];
            }
            updatedFlights[updatedFlights.Length - 1] = flight;

            return updatedFlights;
        }

        static void PrintAirports(string[] airports)
        {
            Console.Write("Airports: ");
            if (airports.Length == 0) 
            {
                Console.WriteLine("none");
            }
            else
            {
                Console.WriteLine(String.Join(", ", airports));
            } 
        }

        static void PrintAirlines(string[] airlines)
        {
            Console.Write("Airlines: ");
            if (airlines.Length == 0)
            {
                Console.WriteLine("none");
            }
            else
            {
                Console.WriteLine(String.Join(", ", airlines));
            }
        }

        static void PrintFlights(string[] flights) 
        {
            Console.WriteLine("Flights ");
            if (flights.Length == 0)
            {
                Console.WriteLine("none");
            }
            else
            {
                Console.WriteLine(String.Join(", ", flights));
            }
        }
    }
}