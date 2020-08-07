using System;
using System.ComponentModel.DataAnnotations;

namespace TWWork_v2.Models
{
    public class RFIDRecord
    {
        [Key]
        [Display(Name = "编号")]
        public int No { get; set; }

        [Display(Name = "RFID设备名称")]
        public string RFIDDeviceName { get; set; }

        [Display(Name = "RFID BoxId")]
        public string RFIDBoxId { get; set; }

        [Display(Name = "RFID站点名称")]
        public string RFIDStationName { get; set; }

        [Display(Name = "RFID创建时间")]
        public DateTime? CreateDate { get; set; }
    }
}