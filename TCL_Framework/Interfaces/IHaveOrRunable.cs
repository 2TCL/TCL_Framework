using System.Collections.Generic;

namespace TCL_Framework.Interfaces
{
    public interface IHaveOrRunable<T> where T: new() 
    {
        IGroupable<T> Having(string condition);
        List<T> Run();
    }
}