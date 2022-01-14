using System.Collections.Generic;

namespace TCL_Framework.Interfaces
{
    public interface IGroupable<T> where T: new()
    {
        IHavingable<T> GroupBy(string columnNames);
        
        List<T> Run();
    }
}
