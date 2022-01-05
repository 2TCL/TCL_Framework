using System;

namespace TCL_Framework.Attributes
{
    public class TableAttribute : Attribute
    {
        public string Name { get; private set; }
        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}
