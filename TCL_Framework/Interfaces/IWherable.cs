using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TCL_Framework.Interfaces
{
    public interface IWherable<T> : IRunable<T> where T:new ()
    {
        IGroupable<T> Where(Expression<Func<T, bool>> expression);
        IGroupable<T> AllRow();
    }
}