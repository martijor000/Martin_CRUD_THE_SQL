using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Martin_CRUD_THE_SQL.Models
{
    public static class SessionExtensions
    {
        public static Product Get<Product>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(Product) : JsonSerializer.Deserialize<Product>(value);
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            var valueAsJson = JsonSerializer.Serialize(value);
            session.SetString(key, valueAsJson);
        }
    }
}
