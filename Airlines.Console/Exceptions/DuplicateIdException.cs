namespace Airlines.Console.Exceptions;

public class DuplicateIdException : Exception
{
    public DuplicateIdException(string message) : base(message)
    {
    }
}