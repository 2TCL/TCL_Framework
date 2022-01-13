using System.Collections.Generic;
using TCL_Framework.Attributes;
using TCL_Framework.Enums;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLInsertQuery<T> : TCLPostgreSQLQuery where T : new()
    {
        public TCLPostgreSQLInsertQuery(string connectionString, T obj) : base(connectionString)
        {
            TCLPostgreSQLMapper mapper = new TCLPostgreSQLMapper();

            string tableName = mapper.GetTableName<T>();

            List<PrimaryKeyAttribute> primaryKeys = mapper.GetPrimaryKeys<T>();
            Dictionary<ColumnAttribute, object> columnsWithValues = mapper.GetColumnsWithValues<T>(obj);

            if (columnsWithValues.Count != 0)
            {
                string column = string.Empty;
                string value = string.Empty;

                foreach (ColumnAttribute columnAttribute in columnsWithValues.Keys)
                {
                    bool isAutoId = false;
                    foreach (PrimaryKeyAttribute primaryKeyAttribute in primaryKeys)
                    {
                        if (columnAttribute.Name == primaryKeyAttribute.Name && primaryKeyAttribute.AutoId)
                        {
                            isAutoId = true;
                            break;
                        }
                    }

                    if (!isAutoId)
                    {
                        string format = "{0}, ";
                        if (columnAttribute.Type == DataType.NCHAR || columnAttribute.Type == DataType.NVARCHAR)
                        {
                            format = "N'{0}', ";
                        } 
                        else if (columnAttribute.Type == DataType.CHAR || columnAttribute.Type == DataType.VARCHAR)
                        {
                            format = "'{0}', ";
                        }
                        column += string.Format("{0}, ", columnAttribute.Name);
                        value += string.Format(format, columnsWithValues[columnAttribute]);
                    }
                }

                if (!string.IsNullOrEmpty(column) && !string.IsNullOrEmpty(value))
                {
                    column = column.Substring(0, column.Length - 2);
                    value = value.Substring(0, value.Length - 2);
                    query = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, column, value);
                }
            }
        }
    }
}
