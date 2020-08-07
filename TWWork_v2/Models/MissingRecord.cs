using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class MissingRecord
	{
		[Key]
		public int No { get; set; }

		[Display(Name = "设备名")]
		public string DeviceName { get; set; }

		[Display(Name = "工序")]
		public string CraftName { get; set; }

		[Display(Name = "站点")]
		public string RFIDStationName { get; set; }

		[Display(Name = "上升沿时间")]
		public DateTime OBTUpTime { get; set; }

		[Display(Name = "下降沿时间")]
		public DateTime OBTDownTime { get; set; }

		[Display(Name = "时间间隔")]
		public string OnTimespan { get; set; }

		[Display(Name = "标记")]
		public string Remark { get; set; }

		[Display(Name = "创建时间")]
		public DateTime CreateDate { get; set; }

		[Display(Name = "出/入站")]
		public string PushOrPop { get; set; }
	}
}
