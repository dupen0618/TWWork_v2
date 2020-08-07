using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWWwork.Models
{
	public class OBTRIFDCntDiff
	{
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public string DeviceName { get; set; }
		public string StationName { get; set; }
		public List<int> RFIDCnt { get; set; }
	}
}
