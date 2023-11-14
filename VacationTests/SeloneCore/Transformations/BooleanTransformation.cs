using Kontur.Selone.Properties;

namespace SeloneCore.Transformations;

public class BooleanTransformation : IPropTransformation<bool>
{
    public bool Deserialize(string value)
    {
        return bool.Parse(value.ToLower());
    }

    public string Serialize(bool value)
    {
        return value.ToString().ToLower();
    }
}