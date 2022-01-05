using System;

namespace TCL_Framework.Attributes
{
    public class ManyToOneAttribute : Attribute
    {
        public string RelationshipId { get; private set; }
        public string TableName { get; private set; }

        public ManyToOneAttribute(string relationshipId, string tableName)
        {
            RelationshipId = relationshipId;
            TableName = tableName;
        }
    }
}
