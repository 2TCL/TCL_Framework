using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL_Framework.Interfaces
{
    public interface IGroupable<T> where T: new()
    {
        IRunable<T> GroupBy(string collumnName);
    }
}
