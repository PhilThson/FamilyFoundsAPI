using System.ComponentModel;
using System.Reflection;

namespace FamilyFoundsApi.Core.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum enumValue)
    {
        var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault() ??
            throw new ArgumentException("Wartość wyliczenia nie istnieje", nameof(enumValue));

        var descAtt = member.GetCustomAttribute<DescriptionAttribute>() ??
            throw new ArgumentException("Wyliczenie nie posiada atrybutu Description", nameof(enumValue));
        
        return descAtt.Description;
    }
}
