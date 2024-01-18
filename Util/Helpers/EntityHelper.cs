using Util.CustomAttributes;

namespace Util.Helpers
{
    public static class EntityHelper
    {
        public static string FormatEntityNameForSql<T>(string suffixToRemove)
        {
            var entityName = typeof(T).Name;
            if (entityName.EndsWith(suffixToRemove))
            {
                return entityName.Substring(0, entityName.Length - suffixToRemove.Length);
            }
            return entityName;
        }

        public static (string TableName, string SchemaName) GetTableInfo<T>()
        {
            var attribute = typeof(T).GetCustomAttributes(typeof(TableInfoAttribute), false)
                                     .FirstOrDefault() as TableInfoAttribute;

            if (attribute == null)
                throw new InvalidOperationException($"The TableInfo attribute is not applied to the {typeof(T).Name} class.");

            return (attribute.TableName, attribute.SchemaName);
        }

    }
}
