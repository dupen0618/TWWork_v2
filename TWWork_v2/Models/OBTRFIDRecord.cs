using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class OBTRFIDRecord
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
