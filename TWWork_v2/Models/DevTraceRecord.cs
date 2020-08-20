using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWWork_v2.Models
{
	public class DevTraceRecord
	{
		[Key]
		public int Id{get;set;}
		public string DeviceName { get; set; }
		public string PushOrPop { get; set; }
		public double? traceCordACount { get; set; }
		public double? traceCordMCount { get; set; }
	}
}
