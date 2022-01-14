using TCL_Framework.Abstractions;
using Npgsql;
using TCL_Framework.Interfaces;
using System.Collections.Generic;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLConnection : TCLConnection
    {
        private NpgsqlConnection _connection;
        private static TCLPostgreSQLConnection _instance;

        private TCLPostgreSQLConnection(string connectionString)
        {
            this.connectionString = connectionString;
            _connection = new NpgsqlConnection(connectionString);
            // this.Open();
        }

        public static TCLPostgreSQLConnection GetInstance(string connectionString)
        {
            return _instance ?? (_instance = new TCLPostgreSQLConnection(connectionString));
        }

        public NpgsqlConnection GetConnection()
        {
            return _connection;
        }

        public override void Open()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public override void Close()
        {
            if (_connection.State != System.Data.ConnectionState.Open) return;
            _connection.Close();
        }

        public override IWherable<T> Select<T>()
        {
            return TCLPostgreSQLSelectQuery<T>.Create(connectionString);
        }

        public override int Insert<T>(T obj)
        {
            TCLPostgreSQLInsertQuery<T> query = new TCLPostgreSQLInsertQuery<T>(connectionString, obj);
            return query.ExecuteNonQuery();
        }

        public override int Update<T>(T obj)
        {
            TCLPostgreSQLUpdateQuery<T> query = new TCLPostgreSQLUpdateQuery<T> (connectionString, obj);
            return query.ExecuteNonQuery();
        }

        public override int Delete<T>(T obj)
        {
            TCLPostgreSQLDeleteQuery<T> query = new TCLPostgreSQLDeleteQuery<T>(connectionString, obj);
            return query.ExecuteNonQuery();
        }

        public override List<T> ExecuteQuery<T>(string query)
        {
            TCLPostgreSQLQuery newQuery = new TCLPostgreSQLQuery(query, connectionString);
            return newQuery.ExecuteQuery<T>();
        }

        public override List<T> ExecuteQueryNoRelationship<T>(string query)
        {
            TCLPostgreSQLQuery newQuery = new TCLPostgreSQLQuery(query, connectionString);
            return newQuery.ExecuteQueryNoRelationship<T>();
        }

        public override int ExecuteNonQuery(string query)
        {
            TCLPostgreSQLQuery newQuery = new TCLPostgreSQLQuery(query, connectionString);
            return newQuery.ExecuteNonQuery();
        }
    }
}
