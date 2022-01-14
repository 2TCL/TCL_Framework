using TCL_Framework.Attributes;
using TCL_Framework.Enums;

namespace TCL_Framework
{
    [Table("student")]
    public class Student
    {
        [PrimaryKey("id", false)]
        [Column("id", DataType.INT)]
        public int Id { get; set; }

        [Column("name", DataType.NVARCHAR)]
        public string Name { get; set; }

        [Column("age", DataType.INT)] 
        public int Age { get; set; }
        
        [Column("class_id", DataType.INT)] 
        public int ClassId { get; set; }

        [ManyToOne("1", "class")]
        [ForeignKey("1","class_id","id")]
        public Classroom Classroom { get; set; }

        public override string ToString()
        {
            return Id + "-" + Name + "-" + Age + "-" + Classroom;
        }
    }
}