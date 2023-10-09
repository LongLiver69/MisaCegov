using System.ComponentModel;
using System.Reflection;

namespace MISA.WEB05.CEGOV.Domain;

/// <summary>
/// Lấy ra attribute Description của Enum
/// Tham khảo: https://stackoverflow.com/questions/17380900/enum-localization
/// </summary>
public static class EnumExtensions
{
    public static string GetDescription(this Enum enumValue)
    {
        var fi = enumValue.GetType().GetField(enumValue.ToString());

        var attributes =
            (DescriptionAttribute[])(fi.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false));

        if (attributes != null &&
            attributes.Length > 0)
            return attributes[0].Description;
        else
            return enumValue.ToString();
    }
}
