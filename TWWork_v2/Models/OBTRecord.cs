using System;
using System.ComponentModel.DataAnnotations;

namespace TWWork_v2.Models
{
    public class OBTRecord
    {
        [Key]
        [Display(Name = "编号")]
        public int No { get; set; }

        [Display(Name = "光电设备名称")]
        public string OBTDeviceName { get; set; }

        [Display(Name = "光电站点名称")]
        public string OBTStationName { get; set; }

        [Display(Name = "光电上升沿时间")]
        public DateTime? UpTime { get; set; }

        [Display(Name = "光电下降沿时间")]
        public DateTime? DownTime { get; set; }

        [Display(Name = "光电时间间隔")]
        public string OBTOnTimespan { get; set; }
    }
}