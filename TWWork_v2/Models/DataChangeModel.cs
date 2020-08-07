using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class DataChangeModel
	{
		[Display(Name = "机台")]
		public string CraftName { get; set; }

		[Key]
		[Display(Name = "设备名")]
		public string DeviceName { get; set; }

		[Display(Name = "站点")]
		public string RFIDStationName { get; set; }

		[Display(Name = "RFID计数")]
		public int? RFIDCnt { get; set; }

		[Display(Name = "漏读数")]
		public int? MissCnt { get; set; }

		[Display(Name = "日期")]
		public string Time { get; set; }

		[Display(Name = "成功率")]
		public decimal? SuccessPercenter { get; set; }
	}
}
