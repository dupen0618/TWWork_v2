using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class SuccessRateModel
	{
		[Key]
		public int No { get; set; }

		[Display(Name = "工序")]
		public string CraftName { get; set; }

		[Display(Name = "设备名")]
		public string DeviceName { get; set; }

		[Display(Name = "RFID站点")]
		public string RFIDStationName { get; set; }

		[Display(Name = "RFID数量")]
		public int RFIDCnt { get; set; }

		[Display(Name = "漏读数量")]
		public int MissCount { get; set; }

		[Display(Name = "成功率")]
		public decimal SuccessPercenter { get; set; }

	}
}
