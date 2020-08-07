using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TWWork_v2.Dao;
using TWWork_v2.Reps.IRepository;
using TWWwork.Models;

namespace TWWork_v2.Reps.Repository
{
    public class DataChangeModelRepository:IDataChangeModelRepository
    {
        private readonly AppDbContext _context;

        public DataChangeModelRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DataChangeModel> GetDataChangeModels(string dateMin, string dateMax)
        {
            List<DataChangeModel> list = null;
            try
            {
                string sql = GetSql(dateMin, dateMax);
                list = _context.DataChangeModels.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.Write("GetDataChangeModels出错:" + ex.Message);
            }
            return list;
        }

        private string GetSql(string dateMin, string dateMax)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT RFIDSuccCount.craftname, RFIDSuccCount.devicename, RFIDSuccCount.RFIDstationname, RFIDSuccCount.RFIDCNT, MissCNT ");
            sql.Append(" 	, RFIDSuccCount.time ");
            sql.Append(" 	, TRUNC((RFIDSuccCount.RFIDCNT - CASE  ");
            sql.Append(" 		WHEN MissCNT IS NULL THEN 0 ");
            sql.Append(" 		ELSE MissCNT ");
            sql.Append(" 	END) / RFIDSuccCount.RFIDCNT, 3) * 100 AS successPercenter ");
            sql.Append(" FROM ( ");
            sql.Append(" 	SELECT craftname, devicename, RFIDstationname, COUNT(*) AS RFIDCNT ");
            sql.Append(" 		, to_char(createdate, 'yyyy-mm-dd') AS time ");
            sql.Append(" 	FROM DEV_TRACERECORD ");
            sql.Append($" 	WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($" 		AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append(" 	GROUP BY craftname, devicename, RFIDstationname, to_char(createdate, 'yyyy-mm-dd') ");
            sql.Append(" ) RFIDSuccCount ");
            sql.Append(" 	LEFT JOIN ( ");
            sql.Append(" 		SELECT craftname, devicename, RFIDstationname, COUNT(*) AS MissCNT ");
            sql.Append(" 			, to_char(createdate, 'yyyy-mm-dd') AS time ");
            sql.Append(" 		FROM DEV_MISSINGRECORD ");
            sql.Append($" 		WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($" 			AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append(" 		GROUP BY craftname, devicename, RFIDstationname, to_char(createdate, 'yyyy-mm-dd') ");
            sql.Append(" 	) RFIDMissCount ON RFIDSuccCount.devicename = RFIDMissCount.devicename ");
            sql.Append(" AND RFIDSuccCount.RFIDstationname = RFIDMissCount.RFIDstationname ");
            sql.Append(" AND RFIDSuccCount.time = RFIDMissCount.time  ");
            sql.Append(" ORDER BY  RFIDSuccCount.time, DECODE(RFIDSuccCount.craftname, '清洗制绒', 1, '扩散', 2, '激光SE', 3, '刻蚀', 4, '退火', 5, '背钝化', 6, '正镀膜', 7, '背镀膜', 8, '丝网', 9), RFIDSuccCount.devicename, RFIDSuccCount.RFIDstationname ");
            return sql.ToString();
        }
        
        
    }
}