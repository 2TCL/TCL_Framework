using System;
using System.Collections.Generic;
using TCL_Framework.Interfaces;
using Npgsql;
using System.Data;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLQuery : TCLQuery
    {
        protected string connectionString;
        protected NpgsqlCommand command;
        protected string query;

        public TCLPostgreSQLQuery(string query, string connectionString)
        {
            this.connectionString = connectionString;
            this.query = query;
            command = new NpgsqlCommand();
            command.Connection = TCLPostgreSQLConnection.GetInstance(connectionString).GetConnection();
        }
        public TCLPostgreSQLQuery(string connectionString)
        {
            this.connectionString = connectionString;
            command = new NpgsqlCommand();
            command.Connection = TCLPostgreSQLConnection.GetInstance(connectionString).GetConnection();
        }

        public List<T> ExecuteQuery<T>() where T : new()
        {
            command.CommandText = query;

            DataTable dataTable = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            adapter.Fill(dataTable);

            List<T> list = new List<T>();
            TCLPostgreSQLConnection connection = TCLPostgreSQLConnection.GetInstance(connectionString);

            TCLPostgreSQLMapper mapper = new TCLPostgreSQLMapper();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(mapper.RelationshipMapping<T>(connection, dataRow));
            }

            return list;
        }

        public List<T> ExecuteQueryWithOutRelationship<T>() where T : new()
        {
            command.CommandText = query;

            DataTable dataTable = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            adapter.Fill(dataTable);

            List<T> list = new List<T>();
            TCLPostgreSQLConnection connection = TCLPostgreSQLConnection.GetInstance(connectionString);

            TCLPostgreSQLMapper mapper = new TCLPostgreSQLMapper();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(mapper.NoRelationshipMapping<T>(connection, dataRow));
            }

            return list;
        }

        public int ExecuteNonQuery()
        {
            command.CommandText = query;
            return command.ExecuteNonQuery();
        }
    }
}
