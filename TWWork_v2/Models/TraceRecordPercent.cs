using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWWork_v2.Models
{
	public class TraceRecordPercent
	{
		public TraceRecordPercent(){
			Items = new List<Item>();
		}
		
		public string Name { get; set; }

		public List<Item> Items { get; set; }

	}

	public class Item
	{
		public int SiteNo { get; set; }
		public string InPowerOnPercent { get; set; }
		public string OutPowerOnPercent { get; set; }
	}
}
