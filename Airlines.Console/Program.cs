
namespace Airlines
{
    public class Program
    {
        public static void Main()
        {
            var airports = new string[10000];
            var airlines = new string[10000];
            var flights = new string[10000];

            airports = ReadInput("Airport", airports);
            airlines = ReadInput("Airline", airlines);
            flights = ReadInput("Flight", flights);

            PrintData("Airport", airports);
            PrintData("Airline", airlines);
            PrintData("Flight", flights);

            SortAirports(airports);
            SortAirlines(airlines);
            SortFlights(flights);

            PrintData("Airport", airports);
            PrintData("Airline", airlines);
            PrintData("Flight", flights);

            Search(airports, airlines, flights);
        }

        public static string[] ReadInput(string type, string[] currentData)
        {
            var newData = currentData;

            Console.WriteLine($"Enter {type} name, or type 'done' to finish:\n");

            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine(" Error: The name cannot be null or empty!");
                    continue;
                }

                if (input == "done")
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

        public static bool Validate(string value, string[] values)
        {
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine(" Error: Name cannot be null or empty!");
                return false;
            }

            if (LinearSearch(values, value) >= 0)
            {
                Console.WriteLine($" Error: The name already exist!");
                return false;
            }

            return true;
        }

        public static bool ValidateAirport(string airport, string[] airports)
        {
            if (!Validate(airport, airports))
            {
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

        public static bool ValidateAirline(string airline, string[] airlines)
        {
            if (!Validate(airline, airlines))
            {
                return false;
            };

            if (airline.Length >= 6)
            {
                Console.WriteLine($" Error: Airline name '{airline}' must be less than 6 characters long!");
                return false;
            }

            return true;
        }

        public static bool ValidateFlight(string flight, string[] flights)
        {
            if (!Validate(flight, flights))
            {
                return false;
            }

            if (!flight.All(char.IsLetterOrDigit))
            {
                Console.WriteLine($" Error: Flight code '{flight}' must contain only alphabetic or numeric characters!");
                return false;
            }

            return true;
        }

        public static string[] AddData(string item, string[] data)
        {

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == null)
                {
                    data[i] = item;

                    break;
                }
            }

            Console.WriteLine($" {item} was added successfully!");

            return data;
        }

        public static void PrintData(string type, string[] data)
        {
            Console.Write($" {type}s: ");

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != null)
                {
                    Console.Write($" {data[i]} ");
                }
            }
            Console.WriteLine();
        }

        public static string[] SortAirports(string[] airports)
        {
            int n = airports.Length;
            string temp;

            for (int j = 0; j < n - 1; j++)
            {
                for (int i = j + 1; i < n; i++)
                {
                    if (string.Compare(airports[j], airports[i]) > 0)
                    {
                        temp = airports[j];
                        airports[j] = airports[i];
                        airports[i] = temp;
                    }
                }
            }

            return airports;
        }

        public static string[] SortAirlines(string[] airlines)
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
                    (airlines[minIndex], airlines[i]) = (airlines[i], airlines[minIndex]);
                }
            }

            return airlines;
        }

        public static string[] SortFlights(string[] flights)
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
                    (flights[minIndex], flights[i]) = (flights[i], flights[minIndex]);
                }
            }

            return flights;
        }

        public static int LinearSearch(string[] array, string target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                {
                    return i; 
                }
            }
            return -1;
        }

        public static int BinarySearch(string[] arr, string target)
        {
            int l = 0, r = arr.Length - 1;
            while (l <= r)
            {
                int m = l + (r - l) / 2;

                int comparisonResult = string.Compare(arr[m], target);

                if (comparisonResult == 0)
                    return m;

                if (comparisonResult < 0)
                    l = m + 1;
                else
                    r = m - 1;
            }

            return -1;
        }

        public static void Search(string[] airports, string[] airlines, string[] flights)
        {
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

                if (searchTerm == "done")
                {
                    Console.WriteLine();
                    break;
                }

                if (BinarySearch(airports, searchTerm) >= 0)
                {
                    termFound = true;
                    Console.WriteLine($" {searchTerm} is Airport name.");
                }

                if (BinarySearch(airlines, searchTerm) >= 0)
                {
                    termFound = true;
                    Console.WriteLine($" {searchTerm} is Airline name.");
                }

                if (BinarySearch(flights, searchTerm) >= 0)
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