using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TCL_Framework.Interfaces
{
    public interface IHavingable<T> : IRunable<T> where T: new() 
    {
        IRunable<T> Having(string condition);
    }
}