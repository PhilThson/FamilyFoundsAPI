namespace FamilyFoundsApi.Core.Extensions;

public static class StringExtensions
{
    public static string ToLowerString(object? value)
    {
        if (value is null)
        {
            return "";
        }
        var result = value.ToString();
        return string.IsNullOrEmpty(result) ? "" : result.ToLower();
    }
}
