using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TCL_Framework.Interfaces
{
    public interface IHavingable<T> where T: new() 
    {
        IRunable<T> Having(Expression<Func<T, bool>> expression);
        List<T> Run();
    }
}