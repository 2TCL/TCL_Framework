using System;

namespace TCL_Framework.Attributes
{
    public class ForeignKeyAttribute : Attribute
    {
        public string RelationshipId { get; private set; }
        public string Name { get; private set; }
        public string References { get; private set; }
        public ForeignKeyAttribute(string relationshipId, string name, string references)
        {
            RelationshipId = relationshipId;
            Name = name;
            References = references;
        }

    }
}
