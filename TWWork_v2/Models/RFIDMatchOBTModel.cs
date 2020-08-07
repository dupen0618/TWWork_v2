using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class RFIDMatchOBTModel
	{
		public string OBTDeviceName { get; set; }
		public string OBTStationName { get; set; }
		public string UpTime { get; set; }
		public string DownTime { get; set; }
		public string OnTimespan { get; set; }
		public string RFIDDeviceName { get; set; }
		public string BoxId { get; set; }
		public string RFIDStationName { get; set; }
		public string CreateDate { get; set; }
	}
}
