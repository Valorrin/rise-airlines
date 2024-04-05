namespace Airlines.Business.Exceptions;
public class InvalidTicketReservationException : Exception
{
    public InvalidTicketReservationException(string message) : base(message)
    {
    }
}