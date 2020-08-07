using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWWork_v2.Models
{
    public class TableInfo
    {
        [Key]
        [Display(Name = "表名")]
        [Column("Segment_Name")]
        public string TableName { get; set; }
        
        [Display(Name = "大小(MB)")]
        [Column("tabSize")]
        public decimal Size { get; set; }
        
        [Display(Name = "记录行数")]
        [Column("num_rows")]
        public decimal RowNum { get; set; }
    }
}