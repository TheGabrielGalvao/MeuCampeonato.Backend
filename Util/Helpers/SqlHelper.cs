using System.Reflection;

namespace Util.Helpers
{
    public static class SqlHelper
    {
        public static IEnumerable<string> GetPropertyNames<T>(params string[] excludedProperties)
        {
            return typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(p => p.Name)
                .Where(name => !excludedProperties.Contains(name));
        }
    }
}
