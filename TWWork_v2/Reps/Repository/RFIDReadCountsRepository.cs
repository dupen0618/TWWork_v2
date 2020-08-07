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
    public class RFIDReadCountsRepository : IRFIDReadCountsRepository
    {
        private readonly AppDbContext _context;

        public RFIDReadCountsRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<RFIDReadCounts> GetRFIDReadCounts(string dateMin,
            string dateMax, string deviceName, string stationName)
        {
            List<RFIDReadCounts> list = new List<RFIDReadCounts>();
            try
            {
                string sql = GetSql(dateMin, dateMax, deviceName, stationName);
                list = _context.RfidReadCountses.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetRFIDReadCounts出错:{ex.Message}");
            }

            return list;
        }

        public string GetSql(string dateMin,
            string dateMax, string deviceName, string stationName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select rownum No,a.* ");
            sql.Append(" from ( select *  ");
            sql.Append(" from dev_rfidreadcounts ");
            sql.Append($" where devicename = '{deviceName}' ");
            sql.Append($" and rfidstationname = '{stationName}' ");
            sql.Append($" and uptime >= '{dateMin}' ");
            sql.Append($" and downtime <= '{dateMax}' ");
            sql.Append(" order by downtime ");
            sql.Append(" ) a ");

            return sql.ToString();
        }
        
    }
}