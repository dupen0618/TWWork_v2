using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public List<DevTraceRecord> FindDevTraceRecordsByDate(string dateMin, string dateMax)
		{
			string sql = GetSql(dateMin, dateMax);
			List<DevTraceRecord> list = new List<DevTraceRecord>();
			try
			{
				list = _context.DevTraceRecords.FromSql(sql).ToList();
			}
			catch (Exception ex)
			{

			}

			return list;
		}

		private string GetSql(string dateMin, string dateMax)
		{
			StringBuilder sql = new StringBuilder();
			sql.AppendLine(" SELECT  ");
			sql.AppendLine("   rownum id,  ");
			sql.AppendLine("   a.devicename,  ");
			sql.AppendLine("   a.PUSHORPOP,  ");
			sql.AppendLine("   a.traceCordACount,  ");
			sql.AppendLine("   m.traceCordMCount  ");
			sql.AppendLine(" FROM  ");
			sql.AppendLine("   ( ");
			sql.AppendLine("     SELECT  ");
			sql.AppendLine("       devicename,  ");
			sql.AppendLine("       PUSHORPOP,  ");
			sql.AppendLine("       t.traceCord,  ");
			sql.AppendLine("       count(t.traceCord) traceCordACount  ");
			sql.AppendLine("     from  ");
			sql.AppendLine("       ( ");
			sql.AppendLine("         SELECT  ");
			sql.AppendLine("           p.*,  ");
			sql.AppendLine("           substr(p.TRACEID,-6, 1) traceCord  ");
			sql.AppendLine("         FROM  ");
			sql.AppendLine("           \"DEV_TRACERECORD\" p  ");
			sql.AppendLine("         where  ");
			sql.AppendLine("           p.createdate >= to_date( ");
			sql.AppendLine($"             '{dateMin}', 'yyyy-MM-dd hh24:mi:ss' ");
			sql.AppendLine("           )  ");
			sql.AppendLine("           and p.createdate <= to_date( ");
			sql.AppendLine($"             '{dateMax}', 'yyyy-MM-dd hh24:mi:ss' ");
			sql.AppendLine("           ) ");
			sql.AppendLine("       ) t  ");
			sql.AppendLine("     group by  ");
			sql.AppendLine("       t.devicename,  ");
			sql.AppendLine("       t.PUSHORPOP,  ");
			sql.AppendLine("       t.traceCord  ");
			sql.AppendLine("     Having  ");
			sql.AppendLine("       t.traceCord = 'A' ");
			sql.AppendLine("   ) a  ");
			sql.AppendLine("   LEFT JOIN ( ");
			sql.AppendLine("     SELECT  ");
			sql.AppendLine("       devicename,  ");
			sql.AppendLine("       PUSHORPOP,  ");
			sql.AppendLine("       t.traceCord,  ");
			sql.AppendLine("       count(t.traceCord) traceCordMCount  ");
			sql.AppendLine("     from  ");
			sql.AppendLine("       ( ");
			sql.AppendLine("         SELECT  ");
			sql.AppendLine("           p.*,  ");
			sql.AppendLine("           substr(p.TRACEID,-6, 1) traceCord  ");
			sql.AppendLine("         FROM  ");
			sql.AppendLine("           \"DEV_TRACERECORD\" p  ");
			sql.AppendLine("         where  ");
			sql.AppendLine("           p.createdate >= to_date( ");
			sql.AppendLine($"             '{dateMin}', 'yyyy-MM-dd hh24:mi:ss' ");
			sql.AppendLine("           )  ");
			sql.AppendLine("           and p.createdate <= to_date( ");
			sql.AppendLine($"             '{dateMax}', 'yyyy-MM-dd hh24:mi:ss' ");
			sql.AppendLine("           ) ");
			sql.AppendLine("       ) t  ");
			sql.AppendLine("     group by  ");
			sql.AppendLine("       t.devicename,  ");
			sql.AppendLine("       t.PUSHORPOP,  ");
			sql.AppendLine("       t.traceCord  ");
			sql.AppendLine("     Having  ");
			sql.AppendLine("       t.traceCord = 'M' ");
			sql.AppendLine("   ) m on a.devicename = m.devicename  ");
			sql.AppendLine("   AND a.PUSHORPOP = m.PUSHORPOP  ");
			sql.AppendLine(" ORDER BY  ");
			sql.AppendLine("   a.devicename ");

			return sql.ToString();
		}
	}
}
