using System;

namespace TCL_Framework.Attributes
{
    public class OneToManyAttribute : Attribute
    {
        public string RelationshipId { get; private set; }
        public string TableName { get; private set; }

        public OneToManyAttribute(string relationshipId, string tableName)
        {
            RelationshipId = relationshipId;
            TableName = tableName;
        }
    }
}
