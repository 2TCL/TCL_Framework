using System.Collections.Generic;

namespace TCL_Framework.Interfaces
{
    interface TCLRepository
    {
        IWhereable<T> Select<T>(T obj) where T: new();
        T Update<T>(T obj) where T : new();
        T Insert<T>(T obj) where T : new();
        void Delete<T>(T obj) where T : new();
        List<T> ExecuteQuery<T>(string query) where T : new();
        List<T> ExecuteQueryWithOutRelationship<T>(string query) where T : new();
        int ExecuteNonQuery(string query);
    }
}
