using Newtonsoft.Json.Serialization;

namespace wms.Utils;

public static class JsonSerializationExtension
{
    private static readonly SnakeCaseNamingStrategy _snakeCaseNamingStrategy
        = new SnakeCaseNamingStrategy();

    public static string ToSnakeCase(this string s)
    {
        if (s == null)
        {
            throw new ArgumentNullException(paramName: nameof(s));
        }
        
        return _snakeCaseNamingStrategy.GetPropertyName(s, false);
    }
}