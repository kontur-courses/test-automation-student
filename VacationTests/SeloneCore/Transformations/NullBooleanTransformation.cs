#nullable enable
using Kontur.Selone.Properties;

namespace SeloneCore.Transformations;

public class NullBooleanTransformation : IPropTransformation<bool>
{
    public bool Deserialize(string? value)
    {
        return !string.IsNullOrEmpty(value) && bool.Parse(value.ToLower());
    }

    public string? Serialize(bool value)
    {
        return !value ? null : value.ToString().ToLower();
    }
}