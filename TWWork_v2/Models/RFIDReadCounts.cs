using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class RFIDReadCounts
	{
		[Display(Name = "编号")]
		public int NO { get; set; }

		[Display(Name = "ID")]
		public int ID { get; set; }

		[Display(Name = "花篮编号")]
		public string BoxID { get; set; }

		[Display(Name = "工艺名")]
		public string DeviceName { get; set; }

		[Display(Name = "设备名")]
		public string CraftName { get; set; }

		[Display(Name = "站点")]
		public string RFIDStationName { get; set; }

		[Display(Name = "触发时间")]
		public string UpTime { get; set; }

		[Display(Name = "离开时间")]
		public string DownTime { get; set; }

		[Display(Name = "持续时间")]
		public string OnTimespan { get; set; }

		[Display(Name = "读取次数")]
		public string ReadCounts { get; set; }

		[Display(Name = "读取速率")]
		public string AveScanTime { get; set; }
	}
}
