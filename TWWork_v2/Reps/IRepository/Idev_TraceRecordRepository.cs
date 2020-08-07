using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWWork_v2.Models;

namespace TWWork_v2.Reps.IRepository
{
	public interface Idev_TraceRecordRepository
	{
		List<dev_TraceRecord> GetDev_TraceRecords(string datemin,string datemax,string craftName);
	}
}
