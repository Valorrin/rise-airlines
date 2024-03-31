namespace Airlines.Console.Exceptions;
public class InvalidCommandArgumentException : Exception
{
    public InvalidCommandArgumentException(string message) : base(message)
    {
    }
}