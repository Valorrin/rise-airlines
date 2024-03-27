namespace Airlines.Business.Utilities;
public class StringHelper
{
    public static string[] SplitBeforeLastElement(string input)
    {
        var lastIndex = input.LastIndexOf(' ');

        var firstPart = input.Substring(0, lastIndex);
        var lastPart = input.Substring(lastIndex + 1);

        return [firstPart, lastPart];
    }
}