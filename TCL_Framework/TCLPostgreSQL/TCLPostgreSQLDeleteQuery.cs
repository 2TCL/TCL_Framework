using System.Collections.Generic;
using TCL_Framework.Attributes;
using TCL_Framework.Enums;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLDeleteQuery<T> : TCLPostgreSQLQuery where T : new()
    {
        public TCLPostgreSQLDeleteQuery(string connectionString, T obj) : base(connectionString)
        {
            TCLPostgreSQLMapper mapper = new TCLPostgreSQLMapper();

            string tableName = mapper.GetTableName<T>();

            List<PrimaryKeyAttribute> primaryKeys = mapper.GetPrimaryKeys<T>();
            Dictionary<ColumnAttribute, object> columnsWithValues = mapper.GetColumnsWithValues<T>(obj);

            string where = string.Empty;

            foreach (PrimaryKeyAttribute primaryKeyAttribute in primaryKeys)
            {
                ColumnAttribute column = mapper.GetColumn(primaryKeyAttribute.Name, columnsWithValues);
                if (column != null)
                {
                    string format = "{0} = {1}, ";
                    if (column.Type == DataType.NCHAR || column.Type == DataType.NVARCHAR)
                        format = "{0} = N'{1}', ";
                    else if (column.Type == DataType.CHAR || column.Type == DataType.VARCHAR)
                        format = "{0} = '{1}', ";

                    where += string.Format(format, primaryKeyAttribute.Name, columnsWithValues[column]);
                }
            }

            if (!string.IsNullOrEmpty(where))
            {
                where = where.Substring(0, where.Length - 2);
                query = $"DELETE FROM {tableName} WHERE {@where}";
            }
        }
    }
}
