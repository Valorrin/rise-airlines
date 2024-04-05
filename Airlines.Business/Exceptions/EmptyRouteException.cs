namespace Airlines.Business.Exceptions;
public class EmptyRouteException : Exception
{
    public EmptyRouteException(string message) : base(message)
    {
    }
}