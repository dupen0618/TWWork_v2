using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	[Table("PF_DEVICEWORKRECORD")]
	public class DeviceWorkRecord
	{
		[Key]
		[Column("ID")]
		public int Id { get; set; }

		[Display(Name = "设备名")]
		[Column("DEVICENAME")]
		public string DeviceName {get;set;}

		[Display(Name = "产线")]
		[Column("CRAFTNAME")]
		public string CraftName { get; set; }

		[Display(Name = "站点")]
		[Column("STATIONNAME")]
		public string StationName { get; set; }

		[Display(Name = "描述")]
		[Column("DESCRIPTION")]
		public string Description { get; set; }

		[Display(Name = "提交时间")]
		[Column("CREATEDATE")]
		public string CreateDate { get; set; }

		[Display(Name = "提交人")]
		[Column("SUBMITPERSON")]
		public string SubmitPerson { get; set; }
	}

	public enum DeviceNameEnum
	{
		Default,
		[Display(Name = "清洗制绒")]
		ZR01,
		[Display(Name = "扩散")]
		KS01,
		[Display(Name = "激光SE")]
		SE01,
		[Display(Name = "刻蚀")]
		KES01,
		[Display(Name = "退火")]
		TH01,
		[Display(Name = "背钝化")]
		WD01,
		[Display(Name = "背镀膜")]
		BM01,
		[Display(Name = "正镀膜")]
		ZM01,
		[Display(Name = "丝网")]
		SJ01
	}
	public enum StationEnum
	{
		Default,IN1, IN2, OUT1, OUT2
	}
}
