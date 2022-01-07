using System.Collections.Generic;

namespace TCL_Framework.Interfaces
{
    public interface IRunable<T> where T: new()
    {
        List<T> Run(string condition);
    }
}
