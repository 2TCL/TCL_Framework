using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL_Framework.Interfaces
{
    interface TCLRepository
    {
        IWhereable<T> Select<T>(T obj) where T: new();
        T Update<T>(T obj) where T : new();
        T Insert<T>(T obj) where T : new();
        void Delete<T>(T obj) where T : new();
    }
}
