namespace Airlines.Console.Exceptions;
public class InvalidTicketReservationException : Exception
{
    public InvalidTicketReservationException(string message) : base(message)
    {
    }
}