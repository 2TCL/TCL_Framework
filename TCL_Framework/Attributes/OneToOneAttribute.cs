﻿using System;

namespace TCL_Framework.Attributes
{
    public class OneToOneAttribute : Attribute
    {
        public string RelationshipId { get; private set; }
        public string TableName { get; private set; }

        public OneToOneAttribute(string relationshipId, string tableName)
        {
            RelationshipId = relationshipId;
            TableName = tableName;
        }
    }
}
