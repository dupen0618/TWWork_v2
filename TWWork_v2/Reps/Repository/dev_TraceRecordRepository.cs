using Microsoft.EntityFrameworkCore;
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
			string sql = $"SELECT * FROM \"DEV_TRACERECORD\" where " +
				$"devicename = '{craftName}' and createdate >=  to_date('{datemin}', 'yyyy-MM-dd hh24:mi:ss') " +
				$"and  createdate <=  to_date('{datemax}', 'yyyy-MM-dd hh24:mi:ss')";
			List<dev_TraceRecord> list = null;
			try
			{
				//list = _context.dev_TraceRecords.Where(e => e.DeviceName == craftName && e.CREATEDATE >= DateTime.Parse(datemin) && e.CREATEDATE <= DateTime.Parse(datemax));
				list = _context.dev_TraceRecords.FromSql(sql).ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return list;
		}
	}
}
