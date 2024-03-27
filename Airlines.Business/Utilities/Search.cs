namespace Airlines.Business.Utilities;
public static class Search
{
    public static int LinearSearch(List<string> data, string target)
    {
        for (var i = 0; i < data.Count; i++)
            if (data[i] == target)
                return i;
        return -1;
    }

    public static int BinarySearch(List<string> data, string target)
    {
        int l = 0, r = data.Count - 1;
        while (l <= r)
        {
            var m = l + ((r - l) / 2);

            var comparisonResult = string.Compare(data[m], target);

            if (comparisonResult == 0)
                return m;

            if (comparisonResult < 0)
                l = m + 1;
            else
                r = m - 1;
        }

        return -1;
    }
}