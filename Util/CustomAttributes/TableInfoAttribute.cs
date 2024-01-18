using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TableInfoAttribute : Attribute
    {
        public string TableName { get; }
        public string SchemaName { get; }

        public TableInfoAttribute(string tableName, string schemaName = "dbo")
        {
            TableName = tableName;
            SchemaName = schemaName;
        }
    }
}
