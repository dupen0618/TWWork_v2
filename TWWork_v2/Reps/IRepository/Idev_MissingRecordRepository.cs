using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWWork_v2.Models;

namespace TWWork_v2.Reps.IRepository
{
	public interface Idev_MissingRecordRepository
	{
		List<dev_MissingRecord> PageList(string dateMin, string dateMax);
	}
}
