
namespace Airlines.Console.Exceptions;
public class InvalidIdCharactersException : Exception
{
    public InvalidIdCharactersException(string message) : base(message)
    {
    }
}