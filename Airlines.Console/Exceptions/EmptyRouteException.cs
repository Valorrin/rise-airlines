namespace Airlines.Console.Exceptions;
public class EmptyRouteException : Exception
{
    public EmptyRouteException(string message) : base(message)
    {
    }
}