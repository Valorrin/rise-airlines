namespace Airlines.Console.Exceptions;
public class InvalidAirportNameException : Exception
{
    public InvalidAirportNameException(string message) : base(message)
    {
    }
}