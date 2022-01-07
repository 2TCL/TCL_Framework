using System;
using System.Collections.Generic;
using TCL_Framework.Interfaces;
using Npgsql;
using System.Data;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLQuery : TCLRepository
    {
        protected string connectionString;
        protected NpgsqlCommand command;
        protected string query;

        public TCLPostgreSQLQuery(string _query, TCLPostgreSQLConnection _connection)
        {
            query = _query;
            command = new NpgsqlCommand();
            command.Connection = TCLPostgreSQLConnection.GetInstance(connectionString).GetConnection();
        }
        public TCLPostgreSQLQuery(TCLPostgreSQLConnection _connection)
        {
            command = new NpgsqlCommand();
            command.Connection = TCLPostgreSQLConnection.GetInstance(connectionString).GetConnection();
        }
        public IWhereable<T> Select<T>(T obj) where T : new()
        {
            throw new NotImplementedException();
        }

        public T Update<T>(T obj) where T : new()
        {
            throw new NotImplementedException();
        }

        public T Insert<T>(T obj) where T : new()
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T obj) where T : new()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string query)
        {
            command.CommandText = query;
            return command.ExecuteNonQuery();
        }

        public List<T> ExecuteQuery<T>(string query) where T : new()
        {
            command.CommandText = query;

            DataTable dataTable = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            adapter.Fill(dataTable);

            List<T> list = new List<T>();
            TCLPostgreSQLConnection connection = TCLPostgreSQLConnection.GetInstance(connectionString);

            //TCLPostgreSQLMapper mapper = TCLPostgreSQLMapper();

            //foreach (DataRow dataRow in dataTable.Rows)
            //{
            //    list.Add(mapper.MapWithRelationship<T> (connection, dataRow));
            //}

            return list;
        }

        public List<T> ExecuteQueryWithOutRelationship<T>(string query) where T : new()
        {
            command.CommandText = query;

            DataTable dataTable = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            adapter.Fill(dataTable);

            List<T> list = new List<T>();
            TCLPostgreSQLConnection connection = TCLPostgreSQLConnection.GetInstance(connectionString);

            //TCLPostgreSQLMapper mapper = TCLPostgreSQLMapper();

            //foreach (DataRow dataRow in dataTable.Rows)
            //{
            //    list.Add(mapper.MapWithoutRelationship<T> (connection, dataRow));
            //}

            return list;
        }






    }
}
