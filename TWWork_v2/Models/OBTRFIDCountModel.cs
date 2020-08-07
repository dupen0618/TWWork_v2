using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class OBTRFIDCountModel
	{
		[Key]
		[Display(Name = "编号")]
		public int No { get; set; }

		[Display(Name = "机台")]
		public string CraftName { get; set; }

		[Display(Name = "设备名")]
		public string DeviceName { get; set; }

		[Display(Name = "站点")]
		public string RFIDName { get; set; }

		[Display(Name = "光电数量")]
		public decimal? ObtCnt { get; set; }

		[Display(Name = "有光电没有RFID")]
		public decimal? ObtWithoutRfid { get; set; }

		[Display(Name = "光电短触发")]
		public decimal? ObtShortImpuls { get; set; }

		[Display(Name = "有RFID没有光电")]
		public decimal? RfidWithoutOBT { get; set; }

		[Display(Name = "RFID数量")]
		public decimal? RfidCnt { get; set; }

		[Display(Name = "计数差异")]
		public decimal? CntDiff { get; set; }

		[Display(Name = "漏读个数")]
		public decimal? MissCnt { get; set; }

		[Display(Name = "成功率")]
		public decimal? SuccessPercenter { get; set; }
	}
}
