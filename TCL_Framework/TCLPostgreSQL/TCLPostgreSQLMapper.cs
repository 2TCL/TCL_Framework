using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TCL_Framework.Abstractions;
using TCL_Framework.Attributes;
using TCL_Framework.Enums;
using TCL_Framework.Interfaces;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLMapper : TCLMapper
    {
        protected override void OneToManyMapping<T>(TCLConnection cnn, DataRow dr, T obj)
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                var oneToManyAttributes = GetAll(attributes, typeof(OneToManyAttribute));

                if (oneToManyAttributes != null && oneToManyAttributes.Length != 0)
                {
                    foreach (OneToManyAttribute oneToManyAttribute in oneToManyAttributes)
                    {
                        Type type = property.PropertyType;
                        if (type.IsGenericType)
                        {
                            Type itemType = type.GetGenericArguments()[0];
                            TCLPostgreSQLMapper mapper = new TCLPostgreSQLMapper();

                            MethodInfo getTableNameMethod = mapper.GetType().GetMethod("GetTableName")
                               .MakeGenericMethod(new Type[] { itemType });
                            string tableName = getTableNameMethod.Invoke(mapper, null) as string;

                            MethodInfo getForeignKeyAttributeMethod = mapper.GetType().GetMethod("GetForeignKeys")
                                .MakeGenericMethod(new Type[] { itemType });
                            List<ForeignKeyAttribute> foreignKeyAttributes = getForeignKeyAttributeMethod.Invoke(mapper, new object[] { oneToManyAttribute.RelationshipId }) as List<ForeignKeyAttribute>;

                            MethodInfo getColumnAttributeMethod = mapper.GetType().GetMethod("GetColumns")
                                .MakeGenericMethod(typeof(T));
                            List<ColumnAttribute> columnAttributes = getColumnAttributeMethod.Invoke(mapper, null) as List<ColumnAttribute>;

                            string whereStr = string.Empty;
                            if (foreignKeyAttributes != null)
                            {
                                foreach (ForeignKeyAttribute foreignKeyAttribute in foreignKeyAttributes)
                                {
                                    ColumnAttribute column = GetColumn(foreignKeyAttribute.References, columnAttributes);
                                    if (column != null)
                                    {
                                        string format = "{0} = {1}, ";
                                        if (column.Type == DataType.NCHAR || column.Type == DataType.NVARCHAR)
                                            format = "{0} = N'{1}', ";
                                        else if (column.Type == DataType.CHAR || column.Type == DataType.VARCHAR)
                                            format = "{0} = '{1}', ";

                                        whereStr += string.Format(format, foreignKeyAttribute.Name, dr[foreignKeyAttribute.References]);
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(whereStr))
                            {
                                whereStr = whereStr.Substring(0, whereStr.Length - 2);
                                string query = string.Format("SELECT * FROM {0} WHERE {1}", tableName, whereStr);

                                cnn.Open();
                                MethodInfo method = cnn.GetType().GetMethod("ExecuteQueryNoRelationship")
                                .MakeGenericMethod(new Type[] { itemType });
                                property.SetValue(obj, method.Invoke(cnn, new object[] { query }));
                                cnn.Close();
                            }
                        }
                    }
                }
            }
        }
        protected override void ToOneMapping<T>(TCLConnection cnn, DataRow dr, T obj)
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                Type type = property.PropertyType;
                var attributes = property.GetCustomAttributes(false);

                var arr1 = GetAll(attributes, typeof(OneToOneAttribute));
                var arr2 = GetAll(attributes, typeof(ManyToOneAttribute));

                var toOneAttributes = new object[arr1.Length + arr2.Length];
                if (toOneAttributes.Length > 0)
                {
                    arr1.CopyTo(toOneAttributes, 0);
                    arr2.CopyTo(toOneAttributes, arr1.Length);
                }

                if (toOneAttributes != null && toOneAttributes.Length != 0)
                {
                    foreach (var attribute in toOneAttributes)
                    {
                        TCLPostgreSQLMapper mapper = new TCLPostgreSQLMapper();
                        string tableName = string.Empty;
                        string whereStr = string.Empty;
                        string relationshipID = string.Empty;

                        if (attribute.GetType() == typeof(OneToOneAttribute))
                        {
                            relationshipID = (attribute as OneToOneAttribute).RelationshipId;
                            tableName = (attribute as OneToOneAttribute).TableName;
                        }
                        else
                        {
                            relationshipID = (attribute as ManyToOneAttribute).RelationshipId;
                            tableName = (attribute as ManyToOneAttribute).TableName;
                        }

                        MethodInfo getForeignKeyAttributeMethod = mapper.GetType().GetMethod("GetForeignKeys")
                            .MakeGenericMethod(typeof(T));
                        List<ForeignKeyAttribute> foreignKeyAttributes = getForeignKeyAttributeMethod.Invoke(mapper, new object[] { relationshipID }) as List<ForeignKeyAttribute>;

                        MethodInfo getColumnAttributeMethod = mapper.GetType().GetMethod("GetColumns")
                            .MakeGenericMethod(new Type[] { type });

                        List<ColumnAttribute> columnAttributes = getColumnAttributeMethod.Invoke(mapper, null) as List<ColumnAttribute>;

                        if (foreignKeyAttributes != null)
                        {
                            foreach (ForeignKeyAttribute foreignKeyAttribute in foreignKeyAttributes)
                            {
                                ColumnAttribute column = GetColumn(foreignKeyAttribute.References, columnAttributes);
                                if (column != null)
                                {
                                    string format = "{0} = {1}, ";
                                    if (column.Type == DataType.NCHAR || column.Type == DataType.NVARCHAR)
                                        format = "{0} = N'{1}', ";
                                    else if (column.Type == DataType.CHAR || column.Type == DataType.VARCHAR)
                                        format = "{0} = '{1}', ";

                                    whereStr += string.Format(format, foreignKeyAttribute.References, dr[foreignKeyAttribute.Name]);
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(whereStr))
                        {
                            whereStr = whereStr.Substring(0, whereStr.Length - 2);
                            string query = string.Format("SELECT * FROM {0} WHERE {1}", tableName, whereStr);

                            cnn.Open();
                            MethodInfo method = cnn.GetType().GetMethod("ExecuteQueryNoRelationship")
                            .MakeGenericMethod(new Type[] { type });
                            var ienumerable = (IEnumerable)method.Invoke(cnn, new object[] { query });
                            cnn.Close();

                            MethodInfo method2 = mapper.GetType().GetMethod("GetFirst");
                            var firstElement = method2.Invoke(mapper, new object[] { ienumerable });

                            property.SetValue(obj, firstElement);
                        }
                    }
                }
            }
        }

    }
}
