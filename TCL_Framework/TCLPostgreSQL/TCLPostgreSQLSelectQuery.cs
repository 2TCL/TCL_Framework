using System;
using System.Collections.Generic;
using TCL_Framework.Interfaces;
using TCL_Framework.Attributes;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLSelectQuery<T> : TCLPostgreSQLQuery, IWherable<T>, IGroupable<T>, IHaveOrRunable<T>, IRunable<T> where T : new()
    {
        private TCLPostgreSQLSelectQuery(string connectionString) : base(connectionString)
        {
            TCLPostgreSQLMapper mapper = new TCLPostgreSQLMapper();

            query = "SELECT";

            foreach (ColumnAttribute column in mapper.GetColumns<T>())
                query = string.Format("{0} {1},", query, column.Name);

            query = query.Substring(0, query.Length - 1);

            query = string.Format("{0} from {1}", query, mapper.GetTableName<T>());
        }

        public static IWherable<T> Create(string connectionString)
        {
            return new TCLPostgreSQLSelectQuery<T>(connectionString);
        }

        public IHaveOrRunable<T> AllRow()
        {
            return this;
        }

        public IRunable<T> GroupBy(string columnNames)
        {
            query = string.Format("{0} GROUP BY {1}", query, columnNames);
            return this;
        }

        public IGroupable<T> Having(string condition)
        {
            query = string.Format("{0} HAVING {1}", query, condition);
            return this;
        }

        public List<T> Run()
        {
            return ExecuteQuery<T>();
        }

        public IHaveOrRunable<T> Where(string condition)
        {
            query = string.Format("{0} WHERE {1}", query, condition);
            return this;
        }
    }
}
