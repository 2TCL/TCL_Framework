using System.Collections.Generic;
using TCL_Framework.Attributes;
using TCL_Framework.Enums;

namespace TCL_Framework
{
    [Table("class")]
    public class Classroom
    {
        [PrimaryKey("id", false)]
        [Column("id", DataType.INT)]
        public int Id { get; set; }

        [Column("name", DataType.NVARCHAR)]
        public string Name { get; set; }
        
        [OneToMany("1","student")]
        public List<Student> Students { get; set; }

        public override string ToString()
        {
            return Id + "-" + Name;
        }
    }
}