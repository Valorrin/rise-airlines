namespace Airlines.Business.Exceptions;
public class InvalidIdCharactersException : Exception
{
    public InvalidIdCharactersException(string message) : base(message)
    {
    }
}