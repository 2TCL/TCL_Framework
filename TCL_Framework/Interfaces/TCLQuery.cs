using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL_Framework.Interfaces
{
    public interface TCLQuery
    {
        List<T> ExecuteQuery<T>() where T : new();
        List<T> ExecuteQueryNoRelationship<T>() where T : new();
        int ExecuteNonQuery();
    }
}
