namespace Airlines.Business.Exceptions;
public class AirportNotFoundException : Exception
{
    public AirportNotFoundException(string message) : base(message)
    {
    }
}