using static FamilyFoundsApi.Core.Extensions.StringExtensions;

namespace FamilyFoundsApi.Core.Helpers;

public static class EntitiesHelper
{
    public static List<string> GetModifiedProperties<T>(T exisit, T update)
    {
        var modifiedProperties = new List<string>();
        var transactionType = typeof(T);
        var propertiesNames = transactionType
            .GetProperties()
            .Where(p => !p.GetAccessors()[0].IsVirtual && p.Name != "Id")
            .Select(p => p.Name);

        foreach (var property in propertiesNames)
        {
            var existValue = ToLowerString(transactionType.GetProperty(property)?.GetValue(exisit));
            var updateValue = ToLowerString(transactionType.GetProperty(property)?.GetValue(update));
            if (updateValue != existValue) {
                modifiedProperties.Add(property);
            }
        }

        return modifiedProperties;
    }
}
