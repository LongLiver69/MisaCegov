using System.ComponentModel;
using System.Resources;

namespace MISA.WEB05.CEGOV.Domain;

/// <summary>
/// Tạo attribute hỗ trợ dùng Resource trong DescriptionAttribute
/// Tham khảo: https://stackoverflow.com/questions/17380900/enum-localization
/// </summary>
public class LocalizedDescriptionAttribute : DescriptionAttribute
{
    private readonly string _resourceKey;
    private readonly ResourceManager _resource;
    public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
    {
        _resourceKey = resourceKey;
        _resource = new ResourceManager(resourceType);
    }

    public override string Description
    {
        get
        {
            var displayName = _resource.GetString(_resourceKey);

            return string.IsNullOrEmpty(displayName)
                ? string.Format("[[{0}]]", _resourceKey)
                : displayName;
        }
    }
}
