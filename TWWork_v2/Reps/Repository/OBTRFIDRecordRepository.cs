using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TWWork_v2.Dao;
using TWWork_v2.Enums;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;
using TWWwork.Models;

namespace TWWork_v2.Reps.Repository
{
    public class OBTRFIDRecordRepository : IOBTRFIDRecordRepository
    {
        private readonly AppDbContext _context;

        public OBTRFIDRecordRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<OBTRFIDRecord> GetOBTRFIDRecords(string dateMin,
            string dateMax, string deviceName, string obtStationName, 
            string rFIDStationName)
        {
            List<OBTRFIDRecord> list = new List<OBTRFIDRecord>();
            try
            {
                string sql = GetSql(dateMin,dateMax,
                    deviceName,obtStationName,rFIDStationName);
                list = _context.ObtrfidRecords.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetOBTRFIDRecords出错:" + ex.Message);
            }

            return list;
        }

        public List<OBTRFIDRecord> GetObtOrRfidRecords(string dateMin, string dateMax, string deviceName,
            StationCategoryEnum stationCategory, string rfidStation)
        {
            List<OBTRFIDRecord> list = new List<OBTRFIDRecord>();
            try
            {
                string sql = "";
                if (stationCategory == StationCategoryEnum.OBT)
                {
                    sql = GetObtSql(dateMin, dateMax, deviceName, rfidStation);
                }
                else if (stationCategory == StationCategoryEnum.RFID)
                {
                    sql = GetRfidSql(dateMin, dateMax, deviceName, rfidStation);
                }
                
                list = _context.ObtrfidRecords.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetOBTRFIDRecords出错:" + ex.Message);
            }

            return list;
        }

        public List<OBTRecord> GetObtRecords(string dateMin, string dateMax, string deviceName, string rfidStation)
        {
            List<OBTRecord> list = new List<OBTRecord>();
            try
            {
                string sql = GetObtSql(dateMin, dateMax, deviceName, rfidStation);
                    
                list = _context.ObtRecords.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetObtRecords出错:" + ex.Message);
            }

            return list;
        }

        public List<RFIDRecord> GetRfidRecords(string dateMin, string dateMax, string deviceName, string rfidStation)
        {
            List<RFIDRecord> list = new List<RFIDRecord>();
            try
            {
                string sql = GetRfidSql(dateMin, dateMax, deviceName, rfidStation);
                    
                list = _context.RfidRecords.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetObtRecords出错:" + ex.Message);
            }

            return list;
        }

        public string GetSql(string dateMin,
            string dateMax, string deviceName, string obtStationName, 
            string rFIDStationName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT rownum NO, ");
            sql.Append("          a.* ");
            sql.Append(" FROM  ");
            sql.Append("     (SELECT * ");
            sql.Append("     FROM  ");
            sql.Append("         (SELECT devicename obtdevicename, ");
            sql.Append("          DECODE(RFIDstationname, ");
            sql.Append("         'RFID01','IN1','RFID02','IN2','RFID03','OUT1','RFID04','OUT2',RFIDstationname) obtstationname, uptime , downtime, ontimespan obtontimespan ");
            sql.Append("         FROM DEV_OBTDETECTRECORD ");
            sql.Append($"         WHERE uptime >= to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"                 AND downtime <= to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"                 AND devicename = '{deviceName}' ");
            sql.Append($"                 AND RFIDstationname = '{obtStationName}' ");
            sql.Append("         ORDER BY  id ) OBTRecord FULL ");
            sql.Append("         JOIN  ");
            sql.Append("             (SELECT devicename rfiddevicename, ");
            sql.Append("          boxid rfidboxid, ");
            sql.Append("          DECODE(RFIDstationname, ");
            sql.Append("         'RFID01','IN1','RFID02','IN2','RFID03','OUT1','RFID04','OUT2',RFIDstationname) RFIDstationname, createdate ");
            sql.Append("             FROM DEV_TRACERECORD ");
            sql.Append($"             WHERE createdate >= to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"                     AND createdate <= to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"                     AND devicename = '{deviceName}' ");
            sql.Append($"                     AND RFIDstationname = '{rFIDStationName}' ");
            sql.Append("             ORDER BY  id ) RFIDRecord ");
            sql.Append("                 ON RFIDRecord.createdate >= OBTRecord.uptime ");
            sql.Append("                     AND RFIDRecord.createdate <= OBTRecord.downtime ");
            sql.Append("             ORDER BY  OBTRecord.downtime,RFIDRecord.createdate  ");
            sql.Append(" 	) a ");

            return sql.ToString();
        }

        public string GetObtSql(string dateMin, string dateMax, string deviceName,
             string rfidStation)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT rownum No, a.*  ");
            sql.Append("	FROM ( SELECT devicename obtDeviceName, DECODE(RFIDstationname,'RFID01','IN1','RFID02','IN2','RFID03','OUT1','RFID04','OUT2',RFIDstationname) obtStationName, uptime, downtime, ontimespan obtontimespan ");
            sql.Append("	FROM DEV_OBTDETECTRECORD ");
            sql.Append($"	WHERE uptime >= to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"		AND downtime <= to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"		AND devicename = '{deviceName}' ");
            sql.Append($"		AND RFIDstationname = '{rfidStation}' ");
            sql.Append("	ORDER BY id ");
            sql.Append(" ) a ");
            
            return sql.ToString();
        }

        public string GetRfidSql(string dateMin, string dateMax, string deviceName,
            string rfidStation)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT rownum No, a.*  ");
            sql.Append("		FROM ( SELECT devicename rfidDeviceName, boxid rfidBoxid, DECODE(RFIDstationname,'RFID01','IN1','RFID02','IN2','RFID03','OUT1','RFID04','OUT2',RFIDstationname) RFIDstationname, createdate ");
            sql.Append("		FROM DEV_TRACERECORD ");
            sql.Append($"		WHERE createdate >= to_date('{dateMin}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"			AND createdate <= to_date('{dateMax}', 'yyyy-mm-dd hh24:mi:ss') ");
            sql.Append($"			AND devicename = '{deviceName}' ");
            sql.Append($"			AND RFIDstationname = '{rfidStation}' ");
            sql.Append("		ORDER BY id ");
            sql.Append(" ) a ");

            return sql.ToString();
        }
    }
}