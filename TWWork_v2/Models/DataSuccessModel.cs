using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class DataSuccessModel
	{
		public DataSuccessModel()
		{
			RFIDCntList = new List<string>();
			MissCntList = new List<string>();
			SuccessPercenterList = new List<string>();
		}

		[Display(Name = "编号")]
		public int No { get; set; }

		[Display(Name = "机台")]
		public string CraftName { get; set; }

		[Display(Name = "设备名")]
		public string DeviceName { get; set; }

		[Display(Name = "站点")]
		public string RFIDStationName { get; set; }

		[Display(Name = "RFID计数")]
		public List<string> RFIDCntList { get; set; }

		[Display(Name = "漏读数")]
		public List<string> MissCntList { get; set; }

		[Display(Name = "成功率")]
		public List<string> SuccessPercenterList { get; set; }

	}
}
