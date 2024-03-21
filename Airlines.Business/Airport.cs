
namespace Airlines.Business;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Airport
{
    private string _id;
    private string _name;
    private string _city;
    private string _country;

    public string Id
    {
        get => _id;
        set
        {
            if (!IsValidId(value))
            {
                throw new ArgumentException("Id cannot be null or empty.");
            }

            _id = value;
        }
    }
    public string Name
    {
        get => _name;
        set
        {
            if (!IsValidString(value))
            {
                throw new ArgumentException("Name can only contain alphabet and space characters.");
            }

            _name = value;
        }
    }

    public string City
    {
        get => _city;
        set
        {
            if (!IsValidString(value))
            {
                throw new ArgumentException("City can only contain alphabet and space characters.");
            }

            _city = value;
        }
    }

    public string Country
    {
        get => _country;
        set
        {
            if (!IsValidString(value))
            {
                throw new ArgumentException("Country can only contain alphabet and space characters.");
            }

            _country = value;
        }
    }

    private static bool IsValidId(string id)
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

    private static bool IsValidString(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }

        foreach (var c in str)
        {
            if (char.IsLetter(c) || c == ' ')
            {
                return true;
            }
        }

        return false;
    }
}
