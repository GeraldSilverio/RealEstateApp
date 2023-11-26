using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RealEstateApp.Core.Application.Helpers
{
    public static class SessionHelper
    {
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
