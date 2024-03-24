namespace Airlines.Business;
public class Flight
{
    private string? _id;
    private string? _departureAirport;
    private string? _arrivalAirport;

    public string Id
    {
        get => _id!;
        set
        {
            if (!IsValidFlightId(value))
            {
                throw new ArgumentException("Id cannot be null or empty.");
            }

            _id = value;
        }
    }

    public string DepartureAirport
    {
        get => _departureAirport!;
        set
        {
            if (!IsValidAirportId(value))
            {
                throw new ArgumentException("Id cannot be null or empty.");
            }

            _departureAirport = value;
        }
    }

    public string ArrivalAirport
    {
        get => _arrivalAirport!;
        set
        {
            if (!IsValidAirportId(value))
            {
                throw new ArgumentException("Id cannot be null or empty.");
            }

            _arrivalAirport = value;
        }
    }

    private static bool IsValidAirportId(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return false;
        }

        if (id.Length is < 2 or > 4)
        {
            return false;
        }

        foreach (var c in id)
        {
            if (!char.IsLetterOrDigit(c))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsValidFlightId(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return false;
        }

        foreach (var c in id)
        {
            if (!char.IsLetterOrDigit(c))
            {
                return false;
            }
        }

        return true;
    }
}