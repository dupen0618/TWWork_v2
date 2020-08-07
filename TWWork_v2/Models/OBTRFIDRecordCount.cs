using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWork_v2.Models
{
	public class OBTRFIDRecordCount
	{
		[Display(Name = "光电工序")]
		public string OBTCraftName { get; set; }

		[Display(Name = "光电机器")]
		public string OBTDeviceName { get; set; }

		[Display(Name = "光电站点")]
		public string OBTDeCode { get; set; }

		[Display(Name = "光电统计")]
		public string OBTCnt { get; set; }

		[Display(Name = "光电抖动计数")]
		public string OBTDisturb { get; set; }

		[Display(Name = "RFID工序")]
		public string RFIDCraftName { get; set; }

		[Display(Name = "RFID机器")]
		public string RFIDDeviceName { get; set; }

		[Display(Name = "RFID站点")]
		public string RFIDDeCode { get; set; }

		[Display(Name = "RFID计数")]
		public string RFIDCnt { get; set; }

		[Display(Name = "RFID计数差异")]
		public string RFIDCntDiff { get; set; }

		[Display(Name = "触发无RFID")]
		public string OBTwithoutRFID { get; set; }
		
		[Display(Name = "光电短触发")]
		public string OBTShortImpuls { get; set; }

		[Display(Name = "RFID无匹配光电")]
		public string RFIDwithoutOBT { get; set; }

		[Display(Name = "RFID与光电完全匹配")]
		public string RFIDMatchOBT { get; set; }
	}
}
