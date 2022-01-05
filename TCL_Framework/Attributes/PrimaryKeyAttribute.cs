using System;

namespace TCL_Framework.Attributes
{
    public class PrimaryKeyAttribute : Attribute
    {
        public string Name { get; private set; }
        public bool AutoId { get; private set; }

        public PrimaryKeyAttribute(string name, bool autoId)
        {
            Name = name;
            AutoId = autoId;
        }
    }
}
