namespace Airlines.Business.Exceptions;
public class InvalidRouteException : Exception
{
    public InvalidRouteException(string message) : base(message)
    {
    }
}