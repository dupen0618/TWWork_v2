using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class SiteReadQuality
	{
		[Key]
		public int No { get; set; }

		[Display(Name = "工序")]
		public string CraftName { get; set; }

		[Display(Name = "设备")]
		public string DeviceName { get; set; }

		[Display(Name = "RFID站点")]
		public string RFIDStation { get; set; }

		[Display(Name = "读取数")]
		public decimal ReadCount { get; set; }

		[Display(Name = "RFID数量")]
		public decimal RFIDNum { get; set; }

		[Display(Name = "平均读取")]
		public decimal AvarageRead { get; set; }

		[Display(Name = "扫描数量<4")]
		public decimal WeakReadCount { get; set; }
	}
}
