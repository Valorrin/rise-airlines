namespace Airlines.Business.Exceptions;
public class InvalidAirportCityException : Exception
{
    public InvalidAirportCityException(string message) : base(message)
    {
    }
}