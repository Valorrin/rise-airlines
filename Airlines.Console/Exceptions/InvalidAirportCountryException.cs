namespace Airlines.Console.Exceptions;
public class InvalidAirportCountryException : Exception
{
    public InvalidAirportCountryException(string message) : base(message)
    {
    }
}