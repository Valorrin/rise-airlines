
namespace Airlines.Business;
public class Airline
{
    private string _id;
    private string _name;
    public string Id
    {
        get => _id;
        set
        {
            if (IsValidId(value))
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
            if (!IsValidName(value))
            {
                throw new ArgumentException("Name can only contain alphabet and space characters.");
            }

            _name = value;
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

    private static bool IsValidName(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }

        foreach (var c in str)
        {
            if (!char.IsLetter(c) && c != ' ')
            {
                return false;
            }
        }

        return true;
    }
}
