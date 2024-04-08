namespace Airlines.Business.Exceptions;
public class InvalidIdLengthException : Exception
{
    public InvalidIdLengthException(string message) : base(message)
    {
    }
}