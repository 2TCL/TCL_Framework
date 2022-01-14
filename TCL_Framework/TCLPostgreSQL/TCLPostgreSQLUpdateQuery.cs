using System.Collections.Generic;
using TCL_Framework.Attributes;
using TCL_Framework.Enums;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLUpdateQuery<T> : TCLPostgreSQLQuery where T : new()
    {
        public TCLPostgreSQLUpdateQuery(string connectionString, T obj) : base(connectionString)
        {
            TCLPostgreSQLMapper mapper = new TCLPostgreSQLMapper();

            string tableName = mapper.GetTableName<T>();

            List<PrimaryKeyAttribute> primaryKeys = mapper.GetPrimaryKeys<T>();
            Dictionary<ColumnAttribute, object> columnsWithValues = mapper.GetColumnsWithValues(obj);

            if (columnsWithValues != null && primaryKeys != null)
            {
                string set = string.Empty;
                string where = string.Empty;

                foreach (ColumnAttribute columnAttribute in columnsWithValues.Keys)
                {
                    string format = "{0} = {1}, ";
                    if (columnAttribute.Type == DataType.NCHAR || columnAttribute.Type == DataType.NVARCHAR)
                        format = "{0} = N'{1}', ";
                    else if (columnAttribute.Type == DataType.CHAR || columnAttribute.Type == DataType.VARCHAR)
                        format = "{0} = '{1}', ";

                    set += string.Format(format, columnAttribute.Name, columnsWithValues[columnAttribute]);
                }

                if (!string.IsNullOrEmpty(set))
                    set = set.Substring(0, set.Length - 2);

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
                    query = $"UPDATE {tableName} SET {set} WHERE {where}";
                }
            }
        }
    }
}
