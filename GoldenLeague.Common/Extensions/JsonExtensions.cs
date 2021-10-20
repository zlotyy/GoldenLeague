using System.Text.Json;

namespace GoldenLeague.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T obj, bool pretify = false)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            var options = pretify ? new JsonSerializerOptions { WriteIndented = true } : new JsonSerializerOptions();
            return JsonSerializer.Serialize(obj, options);
        }
    }
}
