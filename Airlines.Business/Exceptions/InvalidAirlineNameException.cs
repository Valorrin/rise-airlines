namespace Airlines.Business.Exceptions;
public class InvalidAirlineNameException : Exception
{
    public InvalidAirlineNameException(string message) : base(message)
    {
    }
}