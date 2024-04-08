namespace Airlines.Business.Exceptions;
public class InvalidNumberOfArgumentsException : Exception
{
    public InvalidNumberOfArgumentsException(string message) : base(message)
    {
    }
}
