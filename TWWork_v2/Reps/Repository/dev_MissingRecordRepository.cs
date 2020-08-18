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
	public class dev_MissingRecordRepository : Idev_MissingRecordRepository
	{
		private readonly AppDbContext _context;

		public dev_MissingRecordRepository(AppDbContext context)
		{
			_context = context;
		}

		public List<dev_MissingRecord> PageList(string dateMin, string dateMax)
		{
			string sql = "SELECT CRAFTNAME, DEVICENAME, RFIDSTATIONNAME, COUNT(RFIDSTATIONNAME) AS MissingCount " +
				" FROM \"DEV_MISSINGRECORD\" " +
				$" WHERE CREATEDATE >= to_date('{dateMin}', 'yyyy-MM-dd hh24:mi:ss') " +
				$" AND CREATEDATE <= to_date('{dateMax}', 'yyyy-MM-dd hh24:mi:ss') " +
				" GROUP BY CRAFTNAME, DEVICENAME, RFIDSTATIONNAME " +
				" ORDER BY CRAFTNAME, DEVICENAME";

			List<dev_MissingRecord> list = null;
			try
			{
				//list = _context.dev_MissingRecords.FromSql(sql).ToList();
			}
			catch (Exception ex)
			{
				LogManager.WriteLog(LogFile.SQL, $"dev_MissingRecordRepository:PageList:{ex.Message}");
			}


			return list;
		}
	}
}
