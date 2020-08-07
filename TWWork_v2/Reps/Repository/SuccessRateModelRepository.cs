﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TWWork_v2.Dao;
using TWWork_v2.Reps.IRepository;
using TWWwork.Models;

namespace TWWork_v2.Reps.Repository
{
    public class SuccessRateModelRepository : ISuccessRateModelRepository
    {
        private readonly AppDbContext _context;
        
        public SuccessRateModelRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<SuccessRateModel> GetSuccessRateModels(string dateMin, string dateMax)
        {
            List<SuccessRateModel> list = new List<SuccessRateModel>();

            try
            {
	            string sql = GetSql(dateMin, dateMax);
	            list = _context.SuccessRateModels.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
	            Console.WriteLine($"GetSuccessRateModels出错:{ex.Message}");
            }

            return list;
        }

        public string GetSql(string dateMin,string dateMax)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT rownum no,a.*");
			sql.Append(" FROM ( SELECT RFIDSuccCount.craftname, ");
			sql.Append("        RFIDSuccCount.devicename, ");
			sql.Append("        RFIDSuccCount.RFIDstationname, ");
			sql.Append("        RFIDSuccCount.RFIDCNT, ");
			sql.Append("        CASE ");
			sql.Append("            WHEN MissCNT IS NULL THEN 0 ");
			sql.Append("            ELSE MissCNT ");
			sql.Append("        END MissCount, ");
			sql.Append("        TRUNC((RFIDSuccCount.RFIDCNT- (CASE ");
			sql.Append("                                           WHEN MissCNT IS NULL THEN 0 ");
			sql.Append("                                           ELSE MissCNT ");
			sql.Append("                                       END))/RFIDSuccCount.RFIDCNT, 3)*100 AS successPercenter ");
			sql.Append(" FROM ");
			sql.Append("   (SELECT craftname, ");
			sql.Append("           devicename, ");
			sql.Append("           RFIDstationname, ");
			sql.Append("           count(*) RFIDCNT ");
			sql.Append("    FROM DEV_TRACERECORD ");
			sql.Append($"    WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append($"      AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append("    GROUP BY craftname, ");
			sql.Append("             devicename, ");
			sql.Append("             RFIDstationname) RFIDSuccCount ");
			sql.Append(" LEFT JOIN ");
			sql.Append("   (SELECT craftname, ");
			sql.Append("           devicename, ");
			sql.Append("           RFIDstationname, ");
			sql.Append("           count(*) MissCNT ");
			sql.Append("    FROM DEV_MISSINGRECORD ");
			sql.Append($"    WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append($"      AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append("    GROUP BY craftname, ");
			sql.Append("             devicename, ");
			sql.Append("             RFIDstationname) RFIDMissCount ON RFIDSuccCount.craftname = RFIDMissCount.craftname ");
			sql.Append(" AND RFIDSuccCount.devicename = RFIDMissCount.devicename ");
			sql.Append(" AND RFIDSuccCount.RFIDstationname = RFIDMissCount.RFIDstationname ");
			sql.Append(" ORDER BY DECODE(RFIDSuccCount.craftname, '清洗制绒', 1, '扩散', 2, '激光SE', 3, '刻蚀', 4, '退火', 5, '背钝化', 6, '正镀膜', 7, '背镀膜', 8, '丝网', 9), ");
			sql.Append("          RFIDSuccCount.devicename, ");
			sql.Append("          RFIDSuccCount.RFIDstationname ");
			sql.Append(" ) a ");

			return sql.ToString();
        }
    }
}