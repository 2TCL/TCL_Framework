using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL_Framework.Abstractions
{
    public abstract class TCLConnection
    {
        protected string _connectionString { get; set; }
        public abstract void Open();
        public abstract void Close();
    }
}
