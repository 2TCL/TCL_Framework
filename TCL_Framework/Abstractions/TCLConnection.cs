using System.Collections.Generic;
using TCL_Framework.Interfaces;

namespace TCL_Framework.Abstractions
{
    public abstract class TCLConnection
    {
        protected string connectionString { get; set; }
        public abstract void Open();
        public abstract void Close();
        public abstract IWherable<T> Select<T>() where T : new();
        public abstract int Insert<T>(T obj) where T : new();
        public abstract int Update<T>(T obj) where T : new();
        public abstract int Delete<T>(T obj) where T : new();
        public abstract List<T> ExecuteQuery<T>(string query) where T : new();
        public abstract List<T> ExecuteQueryNoRelationship<T>(string query) where T : new();
        public abstract int ExecuteNonQuery(string query);
    }
}
