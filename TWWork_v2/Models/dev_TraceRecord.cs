using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWWork_v2.Models
{
    [Table("DEV_TRACERECORD")]
	public class dev_TraceRecord
	{
        [Key]
        public long ID { get; set; }
        public string BOXID { get; set; }
        [Column("DEVICENAME")]
        public string DeviceName { get; set; }
        public string LASTBOXID { get; set; }
        public string TRACEID { get; set; }
        public string SNUMBER { get; set; }
        public string ORDERID { get; set; }
        public string CRAFTNAME { get; set; }
        public string RFIDSTATIONNAME { get; set; }
        public string PUSHORPOP { get; set; }
        public string REMARK { get; set; }
        public long? CREATEBY { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public long? MODIFIEDBY { get; set; }
        public DateTime? MODIFIEDDATE { get; set; }
    }
}
