using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class InOutCompareDataModel
	{
		[Key]
		[Display(Name = "编号")]
		public int No { get; set; }

		[Display(Name="机台")]
		public string CraftName{ get; set; }

		[Display(Name = "设备名")]
		public string DeviceName { get; set; }

		[Display(Name = "进站FixedCode")]
		public string InBoxId { get; set; }

		[Display(Name="进站名")]
		public string InRFIDStationName { get; set; }

		[Display(Name="进站时间")]
		public DateTime? InCreateDate { get; set; }

		[Display(Name = "出站FixedCode")]
		public string OutBoxId { get; set; }
		
		[Display(Name = "出站名")]
		public string OutRFIDStationName { get; set; }

		[Display(Name="出站时间")]
		public DateTime? OutCreateDate { get; set; }
	}
}
