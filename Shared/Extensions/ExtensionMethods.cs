using Newtonsoft.Json;

namespace Shared.Extensions
{
    public static class ExtensionMethods
    {
        public static string ToJson(this object value)
        {
            if (value == null) return null;

            string json = JsonConvert.SerializeObject(value);
            return json;


        }
    }
}
