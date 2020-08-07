using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class InOutCompareModel
	{
		[Display(Name = "编号")]
		public int ID { get; set; }
		[Display(Name = "设备名")]
		public string DeviceName { get; set; }
		[Display(Name="进料站点的")]
		public string InStationName { get; set; }
		[Display(Name ="进料触发时间")]
		public string InUpTime { get; set; }
		[Display(Name = "进料离开时间")]
		public string InDownTime { get; set; }
		[Display(Name = "进料时间间隔")]
		public string InOnTimespan { get; set; }
		[Display(Name = "进料花篮编号")]
		public string InBoxId { get; set; }
		[Display(Name = "进料创建时间")]
		public string InCreateDate { get; set; }

		[Display(Name = "出料站点")]
		public string OutStationName { get; set; }
		[Display(Name = "出料创建时间")]
		public string OutUpTime { get; set; }
		[Display(Name = "出料离开时间")]
		public string OutDownTime { get; set; }
		[Display(Name = "出料持续时间")]
		public string OutOnTimespan { get; set; }
		[Display(Name = "出料花篮编号")]
		public string OutBoxId { get; set; }
		[Display(Name = "出料创建时间")]
		public string OutCreateDate { get; set; }
	}
}
