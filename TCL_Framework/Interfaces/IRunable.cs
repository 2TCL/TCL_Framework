using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL_Framework.Interfaces
{
    public interface IRunable<T> where T: new()
    {
        List<T> Run(string condition);
    }
}
