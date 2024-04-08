namespace Airlines.Business.Exceptions;
public class InvalidCommandArgumentException : Exception
{
    public InvalidCommandArgumentException(string message) : base(message)
    {
    }
}