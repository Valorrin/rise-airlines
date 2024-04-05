namespace Airlines.Business.Exceptions;
public class InvalidCargoReservationException : Exception
{
    public InvalidCargoReservationException(string message) : base(message)
    {
    }
}