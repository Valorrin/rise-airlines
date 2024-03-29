namespace Airlines.Business.Models;

public class Airport
{
    private string? _id;
    private string? _name;
    private string? _city;
    private string? _country;

    public string Id
    {
        get => _id!;
        set
        {
            if (IsValidId(value))
                _id = value;
        }
    }
    public string Name
    {
        get => _name!;
        set
        {
            if (IsValidString(value))
                _name = value;
        }
    }

    public string City
    {
        get => _city!;
        set
        {
            if (IsValidString(value))
                _city = value;
        }
    }

    public string Country
    {
        get => _country!;
        set
        {
            if (IsValidString(value))
                _country = value;
        }
    }

    private static bool IsValidId(string id)
    {
        if (string.IsNullOrEmpty(id))
            return false;

        if (id.Length is < 2 or > 4)
            return false;

        foreach (var c in id)
            if (!char.IsLetterOrDigit(c))
                return false;

        return true;
    }

    private static bool IsValidString(string str)
    {
        if (string.IsNullOrEmpty(str))
            return false;

        foreach (var c in str)
            if (char.IsLetter(c) || c == ' ')
                return true;

        return false;
    }
}