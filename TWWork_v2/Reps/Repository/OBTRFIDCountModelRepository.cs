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
    public class OBTRFIDCountModelRepository : IOBTRFIDCountModelRepository
    {
        private readonly AppDbContext _context;
        
        public OBTRFIDCountModelRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<OBTRFIDCountModel> GetOBTRFIDCountModels(string dateMin, string dateMax)
        {
            List<OBTRFIDCountModel> list = new List<OBTRFIDCountModel>();

            try
            {
                string sql = GetSql(dateMin, dateMax);
                list = _context.ObtrfidCountModels.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOBTRFIDCountModels出错:{ex.Message}");
            }
            return list;
        }
        
        private static string GetSql(string dateMin, string dateMax)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT rownum no,a.* ");
            sql.Append(" FROM ( SELECT tt.craftname, tt.devicename, tt.RFIDname, tt.OBTCNT, tt.OBTwithoutRFID ");
            sql.Append("  	, tt.OBTSHORTIMPULS, tt.RFIDwithoutOBT, tt.RFIDCNT, tt.CNTDIFF, ss.MissCount missCnt,ss.successPercenter ");
            sql.Append("  FROM ( ");
            sql.Append("  	SELECT oldSQL.craftname, oldSQL.devicename, oldSQL.RFIDname, oldSQL.OBTCNT, newSQL.OBTwithoutRFID ");
            sql.Append("  		, newSQL.OBTSHORTIMPULS, newSQL.RFIDwithoutOBT, oldSQL.RFIDCNT ");
            sql.Append("  		, oldSQL.OBTCNT - newSQL.OBTSHORTIMPULS - oldSQL.RFIDCNT AS CNTDIFF ");
            sql.Append("  	FROM ( ");
            sql.Append("  		SELECT CASE  ");
            sql.Append("  				WHEN OBTRecordCount.craftname IS NOT NULL THEN OBTRecordCount.craftname ");
            sql.Append("  				ELSE RFIDRecordCount.craftname ");
            sql.Append("  			END AS craftname ");
            sql.Append("  			, CASE  ");
            sql.Append("  				WHEN OBTRecordCount.devicename IS NOT NULL THEN OBTRecordCount.devicename ");
            sql.Append("  				ELSE RFIDRecordCount.devicename ");
            sql.Append("  			END AS devicename ");
            sql.Append("  			, CASE  ");
            sql.Append("  				WHEN OBTRecordCount.RFIDstationname IS NOT NULL THEN OBTRecordCount.RFIDstationname ");
            sql.Append("  				ELSE RFIDRecordCount.RFIDstationname ");
            sql.Append("  			END AS RFIDstationname ");
            sql.Append("  			, DECODE(CASE  ");
            sql.Append("  				WHEN OBTRecordCount.RFIDstationname IS NOT NULL THEN OBTRecordCount.RFIDstationname ");
            sql.Append("  				ELSE RFIDRecordCount.RFIDstationname ");
            sql.Append("  			END, 'RFID01', 'IN1', 'RFID02', 'IN2', 'RFID03', 'OUT1', 'RFID04', 'OUT2', 'RFID05', 'NG1', 'RFID06', 'NG2', OBTRecordCount.RFIDstationname) AS RFIDname ");
            sql.Append("  			, OBTRecordCount.OBTCNT, OBTRecordCount.OBTDisturb, RFIDRecordCount.RFIDCNT ");
            sql.Append("  			, OBTRecordCount.OBTCNT - OBTRecordCount.OBTDisturb - RFIDRecordCount.RFIDCNT AS CNTDiff ");
            sql.Append("  		FROM ( ");
            sql.Append("  			SELECT craftname, devicename, RFIDstationname, COUNT(*) AS OBTCNT ");
            sql.Append("  				, SUM(CASE  ");
            sql.Append("  					WHEN downtime = uptime THEN 1 ");
            sql.Append("  					ELSE 0 ");
            sql.Append("  				END) AS OBTDisturb ");
            sql.Append("  			FROM DEV_OBTDETECTRECORD ");
            sql.Append($"  			WHERE uptime > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"  				AND downtime < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append("  			GROUP BY craftname, devicename, RFIDstationname ");
            sql.Append("  		) OBTRecordCount ");
            sql.Append("  			FULL JOIN ( ");
            sql.Append("  				SELECT craftname, devicename, RFIDstationname, COUNT(*) AS RFIDCNT ");
            sql.Append("  				FROM DEV_TRACERECORD ");
            sql.Append($"  				WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"  					AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append("  				GROUP BY craftname, devicename, RFIDstationname ");
            sql.Append("  			) RFIDRecordCount ON OBTRecordCount.devicename = RFIDRecordCount.devicename ");
            sql.Append("  		AND OBTRecordCount.RFIDstationname = RFIDRecordCount.RFIDstationname  ");
            sql.Append("  		ORDER BY DECODE(craftname, '清洗制绒', 1, '扩散', 2, '激光SE', 3, '刻蚀', 4, '退火', 5, '背钝化', 6, '正镀膜', 7, '背镀膜', 8, '丝网', 9), devicename, RFIDstationname ");
            sql.Append("  	) oldSQL ");
            sql.Append("  		FULL JOIN ( ");
            sql.Append("  			SELECT craftname, devicename ");
            sql.Append("  				, DECODE(RFIDstationname, 'RFID01', 'IN1', 'RFID02', 'IN2', 'RFID03', 'OUT1', 'RFID04', 'OUT2', 'RFID05', 'NG1', 'RFID06', 'NG2', RFIDstationname) AS RFIDname ");
            sql.Append("  				, COUNT(*), SUM(CASE  ");
            sql.Append("  					WHEN timespan > 1 ");
            sql.Append("  					AND boxid IS NULL THEN 1 ");
            sql.Append("  					ELSE 0 ");
            sql.Append("  				END) AS OBTwithoutRFID ");
            sql.Append("  				, SUM(CASE  ");
            sql.Append("  					WHEN timespan < 1 THEN 1 ");
            sql.Append("  					ELSE 0 ");
            sql.Append("  				END) AS OBTShortImpuls, SUM(CASE  ");
            sql.Append("  					WHEN downtime IS NULL ");
            sql.Append("  					AND boxid IS NOT NULL THEN 1 ");
            sql.Append("  					ELSE 0 ");
            sql.Append("  				END) AS RFIDwithoutOBT ");
            sql.Append("  				, SUM(CASE  ");
            sql.Append("  					WHEN downtime IS NOT NULL ");
            sql.Append("  					AND boxid IS NOT NULL THEN 1 ");
            sql.Append("  					ELSE 0 ");
            sql.Append("  				END) AS RFIDMatchOBT ");
            sql.Append("  				, COUNT(*) - SUM(CASE  ");
            sql.Append("  					WHEN timespan < 1 THEN 1 ");
            sql.Append("  					ELSE 0 ");
            sql.Append("  				END) - SUM(CASE  ");
            sql.Append("  					WHEN downtime IS NOT NULL ");
            sql.Append("  					AND boxid IS NOT NULL THEN 1 ");
            sql.Append("  					ELSE 0 ");
            sql.Append("  				END) AS CountDiff ");
            sql.Append("  			FROM ( ");
            sql.Append("  				SELECT CASE  ");
            sql.Append("  						WHEN OBTRecord.craftname IS NOT NULL THEN OBTRecord.craftname ");
            sql.Append("  						ELSE RFIDRecord.craftname ");
            sql.Append("  					END AS craftname ");
            sql.Append("  					, CASE  ");
            sql.Append("  						WHEN OBTRecord.devicename IS NOT NULL THEN OBTRecord.devicename ");
            sql.Append("  						ELSE RFIDRecord.devicename ");
            sql.Append("  					END AS devicename ");
            sql.Append("  					, CASE  ");
            sql.Append("  						WHEN OBTRecord.RFIDstationname IS NOT NULL THEN OBTRecord.RFIDstationname ");
            sql.Append("  						ELSE RFIDRecord.RFIDstationname ");
            sql.Append("  					END AS RFIDstationname, OBTRecord.uptime, OBTRecord.downtime ");
            sql.Append("  					, to_char(to_timestamp(ontimespan, 'mi:ss:ff'), 'miss.ff') AS timespan ");
            sql.Append("  					, RFIDRecord.boxid, RFIDRecord.createdate ");
            sql.Append("  				FROM ( ");
            sql.Append("  					SELECT craftname, devicename, RFIDstationname, uptime, downtime ");
            sql.Append("  						, ontimespan ");
            sql.Append("  					FROM DEV_OBTDETECTRECORD ");
            sql.Append($"  					WHERE uptime > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"  						AND uptime < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append("  				) OBTRecord ");
            sql.Append("  					FULL JOIN ( ");
            sql.Append("  						SELECT craftname, devicename, boxid, RFIDstationname, createdate ");
            sql.Append("  						FROM DEV_TRACERECORD ");
            sql.Append($"  						WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"  							AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append("  					) RFIDRecord ON RFIDRecord.devicename = OBTRecord.devicename ");
            sql.Append("  				AND RFIDRecord.RFIDstationname = OBTRecord.RFIDstationname ");
            sql.Append("  				AND RFIDRecord.createdate >= OBTRecord.uptime ");
            sql.Append("  				AND RFIDRecord.createdate <= OBTRecord.downtime  ");
            sql.Append("  				ORDER BY OBTRecord.devicename, OBTRecord.RFIDstationname ");
            sql.Append("  			) matchTable ");
            sql.Append("  			GROUP BY craftname, devicename, RFIDstationname ");
            sql.Append("  			ORDER BY DECODE(craftname, '清洗制绒', 1, '扩散', 2, '激光SE', 3, '刻蚀', 4, '退火', 5, '背钝化', 6, '正镀膜', 7, '背镀膜', 8, '丝网', 9), devicename, RFIDname ");
            sql.Append("  		) newSQL ON oldSQL.craftname = newSQL.craftname ");
            sql.Append("  	AND oldSQL.devicename = newSQL.devicename ");
            sql.Append("  	AND oldSQL.RFIDname = newSQL.RFIDname  ");
            sql.Append("  	ORDER BY DECODE(craftname, '清洗制绒', 1, '扩散', 2, '激光SE', 3, '刻蚀', 4, '退火', 5, '背钝化', 6, '正镀膜', 7, '背镀膜', 8, '丝网', 9), devicename, RFIDname ");
            sql.Append("  ) tt ");
            sql.Append("  	FULL JOIN ( ");
            sql.Append("  		SELECT s.craftname, s.devicename ");
            sql.Append("  			, DECODE(s.RFIDstationname, 'RFID01', 'IN1', 'RFID02', 'IN2', 'RFID03', 'OUT1', 'RFID04', 'OUT2', 'RFID05', 'NG1', 'RFID06', 'NG2', s.RFIDstationname) AS RFIDName ");
            sql.Append("  			, s.RFIDCNT, s.MissCount,s.successPercenter ");
            sql.Append("  		FROM ( ");
            sql.Append("  			SELECT RFIDSuccCount.craftname, RFIDSuccCount.devicename, RFIDSuccCount.RFIDstationname, RFIDSuccCount.RFIDCNT ");
            sql.Append("  				, CASE  ");
            sql.Append("  					WHEN MissCNT IS NULL THEN 0 ");
            sql.Append("  					ELSE MissCNT ");
            sql.Append("  				END AS MissCount ");
            sql.Append("  				, TRUNC((RFIDSuccCount.RFIDCNT - CASE  ");
            sql.Append("  					WHEN MissCNT IS NULL THEN 0 ");
            sql.Append("  					ELSE MissCNT ");
            sql.Append("  				END) / RFIDSuccCount.RFIDCNT, 3) * 100 AS successPercenter ");
            sql.Append("  			FROM ( ");
            sql.Append("  				SELECT craftname, devicename, RFIDstationname, COUNT(*) AS RFIDCNT ");
            sql.Append("  				FROM DEV_TRACERECORD ");
            sql.Append($"  				WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"  					AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append("  				GROUP BY craftname, devicename, RFIDstationname ");
            sql.Append("  			) RFIDSuccCount ");
            sql.Append("  				LEFT JOIN ( ");
            sql.Append("  					SELECT craftname, devicename, RFIDstationname, COUNT(*) AS MissCNT ");
            sql.Append("  					FROM DEV_MISSINGRECORD ");
            sql.Append($"  					WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"  						AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append("  					GROUP BY craftname, devicename, RFIDstationname ");
            sql.Append("  				) RFIDMissCount ON RFIDSuccCount.craftname = RFIDMissCount.craftname ");
            sql.Append("  			AND RFIDSuccCount.devicename = RFIDMissCount.devicename ");
            sql.Append("  			AND RFIDSuccCount.RFIDstationname = RFIDMissCount.RFIDstationname  ");
            sql.Append("  			ORDER BY DECODE(RFIDSuccCount.craftname, '清洗制绒', 1, '扩散', 2, '激光SE', 3, '刻蚀', 4, '退火', 5, '背钝化', 6, '正镀膜', 7, '背镀膜', 8, '丝网', 9), RFIDSuccCount.devicename, RFIDSuccCount.RFIDstationname ");
            sql.Append("  		) s ");
            sql.Append("  	) ss ON tt.devicename = ss.devicename ");
            sql.Append("  AND tt.RFIDname = ss.RFIDName  ");
            sql.Append("  ORDER BY DECODE(tt.craftname, '清洗制绒', 1, '扩散', 2, '激光SE', 3, '刻蚀', 4, '退火', 5, '背钝化', 6, '正镀膜', 7, '背镀膜', 8, '丝网', 9), ");
            sql.Append("   tt.devicename, tt.RFIDname ");
            sql.Append(" ) a");

            return sql.ToString();
        }

    }
}