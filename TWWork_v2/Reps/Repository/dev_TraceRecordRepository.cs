using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWWork_v2.Dao;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;

namespace TWWork_v2.Reps.Repository
{
	public class dev_TraceRecordRepository : Idev_TraceRecordRepository
	{
		private readonly AppDbContext _context;

		public dev_TraceRecordRepository(AppDbContext context)
		{
			_context = context;
		}

		public List<dev_TraceRecord> GetDev_TraceRecords(string datemin, string datemax, string craftName)
		{
			throw new NotImplementedException();
		}
	}
}
