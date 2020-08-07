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
    public class InOutCompareDataModelRepository : IInOutCompareDataModelRepository
    {
        private readonly AppDbContext _context;

        public InOutCompareDataModelRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<InOutCompareDataModel> GetInOutCompareDataModels(string dateMin,
            string dateMax, string deviceName)
        {
            List<InOutCompareDataModel> list = new List<InOutCompareDataModel>();

            try
            {
                string sql = GetSql(dateMin, dateMax, deviceName);
                list = _context.InOutCompareDataModels.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetInOutCompareDataModels出错:{ex.Message}");
            }
            return list;
        }

        private string GetSql(string dateMin, 
            string dateMax, string deviceName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT rownum No,a.* ");
            sql.Append(" FROM ( SELECT ITab.craftname, ITab.devicename, ITab.boxid InBoxId, ITab.InStationName InRFIDStationName ");
            sql.Append(" 	, ITab.createdate InCreateDate, IOTab.OutBoxId,IOTab.OutStationName OutRFIDStationName, IOTab.OutCreateDate ");
            sql.Append(" FROM ( ");
            sql.Append(" 	SELECT INtable.id AS InID, INtable.craftname, INtable.devicename, INtable.boxid, InStationName ");
            sql.Append(" 		, INtable.createdate, OUTtable.id AS OutId, OUTtable.boxid AS OutBoxId, OUTtable.OutStationName, OUTtable.createdate AS OutCreateDate ");
            sql.Append(" 	FROM ( ");
            sql.Append(" 		SELECT id, craftname, devicename, boxid ");
            sql.Append(" 			, DECODE(RFIDstationname, 'RFID01', 'IN1', 'RFID02', 'IN2', 'RFID03', 'OUT1', 'RFID04', 'OUT2', 'RFID05', 'NG1', 'RFID06', 'NG2', RFIDstationname) AS InStationName ");
            sql.Append(" 			, createdate ");
            sql.Append(" 		FROM DEV_TRACERECORD ");
            sql.Append($" 		WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($" 			AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($" 			AND devicename = '{deviceName}' ");
            sql.Append(" 			AND PUSHORPOP = 'IN' ");
            sql.Append(" 	) INtable ");
            sql.Append(" 		FULL JOIN ( ");
            sql.Append(" 			SELECT id, devicename, boxid ");
            sql.Append(" 				, DECODE(RFIDstationname, 'RFID01', 'IN1', 'RFID02', 'IN2', 'RFID03', 'OUT1', 'RFID04', 'OUT2', 'RFID05', 'NG1', 'RFID06', 'NG2', RFIDstationname) AS OutStationName ");
            sql.Append(" 				, createdate ");
            sql.Append(" 			FROM DEV_TRACERECORD ");
            sql.Append($" 			WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($" 				AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($" 				AND devicename = '{deviceName}' ");
            sql.Append(" 				AND PUSHORPOP = 'OUT' ");
            sql.Append(" 		) OUTtable ON INtable.boxid = OUTtable.boxid  ");
            sql.Append(" 	WHERE (OUTtable.createdate - INtable.createdate > 0 ");
            sql.Append(" 			AND (OUTtable.createdate - INtable.createdate) * 24 < 1) ");
            sql.Append(" 		OR OUTtable.createdate IS NULL ");
            sql.Append(" 		OR INtable.createdate IS NULL ");
            sql.Append(" 	ORDER BY INtable.createdate, OUTtable.createdate ASC ");
            sql.Append(" ) IOTab ");
            sql.Append(" 	FULL JOIN ( ");
            sql.Append(" 		SELECT id, craftname, devicename, boxid ");
            sql.Append(" 			, DECODE(RFIDstationname, 'RFID01', 'IN1', 'RFID02', 'IN2', 'RFID03', 'OUT1', 'RFID04', 'OUT2', 'RFID05', 'NG1', 'RFID06', 'NG2', RFIDstationname) AS InStationName ");
            sql.Append(" 			, createdate ");
            sql.Append(" 		FROM DEV_TRACERECORD ");
            sql.Append($" 		WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($" 			AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($" 			AND devicename = '{deviceName}' ");
            sql.Append(" 			AND PUSHORPOP = 'IN' ");
            sql.Append(" 	) ITab ON IOTab.InID = ITab.id  ");
            sql.Append(" ORDER BY ITab.createdate, IOTab.OutCreateDate ASC ");
            sql.Append(" ) a ");

            return sql.ToString();
        }
    }
}