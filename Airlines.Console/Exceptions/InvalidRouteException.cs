namespace Airlines.Console.Exceptions;
public class InvalidRouteException : Exception
{
    public InvalidRouteException(string message) : base(message) { }
}