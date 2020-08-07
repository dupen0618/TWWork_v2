using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.Utils;

namespace TWWork_v2.Reps.Repository
{
    public class OBTRFIDRecordCountRepository : IOBTRFIDRecordCountRepository
    {
        private readonly IConfiguration _configuration;

        public OBTRFIDRecordCountRepository(IConfiguration configurationt)
        {
            _configuration = configurationt;
        }
        
        public List<OBTRFIDRecordCount> GetOBTRFIDRecordCounts(string dateMin, string dateMax)
        {
           string conStr = _configuration.GetConnectionString("TWWorkDbConnection");
			OracleConnection connection = new OracleConnection(conStr);

			string sql = GetSql(dateMin,dateMax);
			List<OBTRFIDRecordCount> oBTRFIDRecordCounts = new List<OBTRFIDRecordCount>();

			using (OracleCommand command = connection.CreateCommand())
			{
				try
				{
					connection.Open();
					command.CommandText = sql.ToString();

					OracleDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						OBTRFIDRecordCount oBTRFIDRecordCount = new OBTRFIDRecordCount();
						oBTRFIDRecordCount.OBTCraftName = DataProcess.GetDataValue(reader.GetValue(0));
						oBTRFIDRecordCount.OBTDeviceName = DataProcess.GetDataValue(reader.GetValue(1));
						oBTRFIDRecordCount.OBTDeCode = DataProcess.GetDataValue(reader.GetValue(2));
						oBTRFIDRecordCount.OBTCnt = DataProcess.GetDataValue(reader.GetValue(3));
						oBTRFIDRecordCount.OBTDisturb = DataProcess.GetDataValue(reader.GetValue(4));
						oBTRFIDRecordCount.RFIDCraftName = DataProcess.GetDataValue(reader.GetValue(5));
						oBTRFIDRecordCount.RFIDDeviceName = DataProcess.GetDataValue(reader.GetValue(6));
						oBTRFIDRecordCount.RFIDDeCode = DataProcess.GetDataValue(reader.GetValue(7));
						oBTRFIDRecordCount.RFIDCnt = DataProcess.GetDataValue(reader.GetValue(8));
						oBTRFIDRecordCount.RFIDCntDiff = DataProcess.GetDataValue(reader.GetValue(9));
						oBTRFIDRecordCounts.Add(oBTRFIDRecordCount);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("GetOBTRFIDRecordCount:" + ex.Message);
				}
				finally
				{
					connection.Close();
				}
			}

			using (OracleCommand command = connection.CreateCommand())
			{
				try
				{
					connection.Open();
					for (int i = 0; i < oBTRFIDRecordCounts.Count; i++)
					{
						command.CommandText = GetSql2(oBTRFIDRecordCounts[i].OBTDeviceName, oBTRFIDRecordCounts[i].OBTDeCode, oBTRFIDRecordCounts[i].RFIDDeviceName, oBTRFIDRecordCounts[i].RFIDDeCode, dateMin, dateMax);
						OracleDataReader reader = command.ExecuteReader();
						while (reader.Read())
						{
							oBTRFIDRecordCounts[i].OBTwithoutRFID = DataProcess.GetDataValue(reader.GetValue(1));
							oBTRFIDRecordCounts[i].OBTShortImpuls = DataProcess.GetDataValue(reader.GetValue(2));
							oBTRFIDRecordCounts[i].RFIDwithoutOBT = DataProcess.GetDataValue(reader.GetValue(3));
							oBTRFIDRecordCounts[i].RFIDMatchOBT = DataProcess.GetDataValue(reader.GetValue(4));
						}

					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("GetOBTRFIDRecordCount3出异常了" + ex.Message);
				}
				finally
				{
					connection.Close();
				}
			}
			return oBTRFIDRecordCounts;
        }
        
        private string GetSql(string dateMin, string dateMax)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append(" SELECT OBTRecordCount.craftname, ");
			sql.Append("        OBTRecordCount.devicename, ");
			sql.Append("        DECODE(OBTRecordCount.RFIDstationname, 'RFID01', 'IN1', 'RFID02', 'IN2', 'RFID03', 'OUT1', 'RFID04', 'OUT2', OBTRecordCount.RFIDstationname), ");
			sql.Append("        OBTRecordCount.OBTCNT, ");
			sql.Append("        OBTRecordCount.OBTDisturb, ");
			sql.Append("        RFIDRecordCount.craftname, ");
			sql.Append("        RFIDRecordCount.devicename, ");
			sql.Append("        DECODE(RFIDRecordCount.RFIDstationname, 'RFID01', 'IN1', 'RFID02', 'IN2', 'RFID03', 'OUT1', 'RFID04', 'OUT2', RFIDRecordCount.RFIDstationname), ");
			sql.Append("        RFIDRecordCount.RFIDCNT, ");
			sql.Append("        OBTRecordCount.OBTCNT -OBTRecordCount.OBTDisturb - RFIDRecordCount.RFIDCNT CNTDiff ");
			sql.Append(" FROM ");
			sql.Append("   (SELECT craftname, ");
			sql.Append("           devicename, ");
			sql.Append("           RFIDstationname, ");
			sql.Append("           count(*) OBTCNT, ");
			sql.Append("           sum(CASE ");
			sql.Append("                   WHEN downtime=uptime THEN 1 ");
			sql.Append("                   ELSE 0 ");
			sql.Append("               END) OBTDisturb ");
			sql.Append("    FROM DEV_OBTDETECTRECORD ");
			sql.Append($"    WHERE uptime > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append($"      AND downtime < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append(" GROUP BY craftname, ");
			sql.Append("          devicename, ");
			sql.Append("          RFIDstationname) OBTRecordCount ");
			sql.Append(" FULL OUTER JOIN ");
			sql.Append("   (SELECT craftname, ");
			sql.Append("           devicename, ");
			sql.Append("           RFIDstationname, ");
			sql.Append("           count(*) RFIDCNT ");
			sql.Append("    FROM DEV_TRACERECORD ");
			sql.Append($"    WHERE createdate > to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append($"      AND createdate < to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append("    GROUP BY craftname, ");
			sql.Append("             devicename, ");
			sql.Append("             RFIDstationname) RFIDRecordCount ON OBTRecordCount.devicename = RFIDRecordCount.devicename ");
			sql.Append(" AND OBTRecordCount.RFIDstationname = RFIDRecordCount.RFIDstationname ");
			sql.Append(" ORDER BY DECODE(RFIDRecordCount.craftname, '清洗制绒', 1, '扩散', 2, '激光SE', 3, '刻蚀', 4, '退火', 5, '背钝化', 6, '正镀膜', 7, '背镀膜', 8, '丝网', 9), ");
			sql.Append("          RFIDRecordCount.devicename, ");
			sql.Append("          RFIDRecordCount.RFIDstationname ");

			return sql.ToString();
		}

		private string GetSql2(string OBTDevicename, string OBTStationname, string RFIDDevicename, string RFIDStationname, string startTime, string endTime)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append(" SELECT COUNT(*), SUM(CASE  ");
			sql.Append(" 		WHEN timespan > 1 ");
			sql.Append(" 		AND boxid IS NULL THEN 1 ");
			sql.Append(" 		ELSE 0 ");
			sql.Append(" 	END) AS OBTwithoutRFID ");
			sql.Append(" 	, SUM(CASE  ");
			sql.Append(" 		WHEN timespan < 1 THEN 1 ");
			sql.Append(" 		ELSE 0 ");
			sql.Append(" 	END) AS OBTShortImpuls, SUM(CASE  ");
			sql.Append(" 		WHEN downtime IS NULL ");
			sql.Append(" 		AND boxid IS NOT NULL THEN 1 ");
			sql.Append(" 		ELSE 0 ");
			sql.Append(" 	END) AS RFIDwithoutOBT ");
			sql.Append(" 	, SUM(CASE  ");
			sql.Append(" 		WHEN downtime IS NOT NULL ");
			sql.Append(" 		AND boxid IS NOT NULL THEN 1 ");
			sql.Append(" 		ELSE 0 ");
			sql.Append(" 	END) AS RFIDMatchOBT ");
			sql.Append(" FROM ( ");
			sql.Append(" 	SELECT * ");
			sql.Append(" 	FROM ( ");
			sql.Append(" 		SELECT devicename, RFIDstationname, uptime, downtime ");
			sql.Append(" 			, to_char(to_timestamp(ontimespan, 'mi:ss:ff'), 'miss.ff') AS timespan ");
			sql.Append(" 		FROM DEV_OBTDETECTRECORD ");
			sql.Append($" 		WHERE (uptime > to_date('{startTime}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append($" 			AND uptime < to_date('{endTime}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append($" 			AND devicename = '{OBTDevicename}' ");
			sql.Append($" 			AND RFIDstationname = '{DataProcess.GetStationName(OBTStationname)}') ");
			sql.Append(" 		ORDER BY id ");
			sql.Append(" 	) OBTRecord ");
			sql.Append(" 		FULL JOIN ( ");
			sql.Append(" 			SELECT devicename, boxid, RFIDstationname, createdate ");
			sql.Append(" 			FROM DEV_TRACERECORD ");
			sql.Append($" 			WHERE (createdate > to_date('{startTime}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append($" 				AND createdate < to_date('{endTime}', 'yyyy-mm-dd hh24:mi:ss') ");
			sql.Append($" 				AND devicename = '{RFIDDevicename}' ");
			sql.Append($" 				AND RFIDstationname = '{DataProcess.GetStationName(RFIDStationname)}') ");
			sql.Append(" 			ORDER BY id ");
			sql.Append(" 		) RFIDRecord ");
			sql.Append(" 		ON RFIDRecord.createdate >= OBTRecord.uptime ");
			sql.Append(" 			AND RFIDRecord.createdate <= OBTRecord.downtime ");
			sql.Append(" 	ORDER BY OBTRecord.downtime ");
			sql.Append(" ) ");
			return sql.ToString();
		}
    }
}