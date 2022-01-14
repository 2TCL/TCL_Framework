using TCL_Framework.Attributes;
using TCL_Framework.Enums;

namespace TCL_Framework
{
    [Table("student")]
    public class StudentGroup
    {
        [Column("COUNT(*) count", DataType.INT)]
        public long Count { get; set; }
        
        [Column("class_id", DataType.INT)]
        public int ClassId { get; set; }

        public override string ToString()
        {
            return Count + "-" + ClassId;
        }
    }
}