using TCL_Framework.Abstractions;
using Npgsql;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLConnection : TCLConnection
    {
        private NpgsqlConnection _connection;
        private static TCLPostgreSQLConnection instance;

        private TCLPostgreSQLConnection(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new NpgsqlConnection(connectionString);
            this.Open();
        }

        public static TCLPostgreSQLConnection GetInstance(string connectionString)
        {
            if (instance == null)
            {
                instance = new TCLPostgreSQLConnection(connectionString);
            }
            return instance;
        }

        public NpgsqlConnection GetConnection()
        {
            return _connection;
        }

        public override void Open()
        {
            if (_connection == null)
            {
                _connection.Open();
            }
        }

        public override void Close()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
                instance = null;
            }
        }


    }
}
