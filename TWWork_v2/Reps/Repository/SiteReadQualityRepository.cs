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
    public class SiteReadQualityRepository : ISiteReadQualityRepository
    {
        private readonly AppDbContext _context;

        public SiteReadQualityRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<SiteReadQuality> GetSiteReadQualitys(string dateMin, string dateMax)
        {
            List<SiteReadQuality> list = new List<SiteReadQuality>();
            try
            {
                string sql = GetSql(dateMin, dateMax);
                list = _context.SiteReadQualities.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetSiteReadQualitys出错了:{ex.Message}");
            }
            
            return list;
        }

        private string GetSql(string dateMin, string dateMax)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT rownum No, a.* ");
            sql.Append(" FROM ( SELECT craftname, ");
            sql.Append("        devicename, ");
            sql.Append("        DECODE(RFIDstationname, '01', 'IN1', '02', 'IN2', '03', 'OUT1', '04', 'OUT2', RFIDstationname) RFIDStation, ");
            sql.Append("        SUM(READCOUNTS) readCount, ");
            sql.Append("        count(*) rfidNum, ");
            sql.Append("        TRUNC(SUM(READCOUNTS)/count(*), 0) avarageRead, ");
            sql.Append("        sum(CASE ");
            sql.Append("                WHEN READCOUNTS <6 THEN 1 ");
            sql.Append("                ELSE 0 ");
            sql.Append("            END) weakReadCount ");
            sql.Append(" FROM DEV_RFIDREADCOUNTS ");
            sql.Append($" WHERE uptime > '{dateMin}' ");
            sql.Append($"   AND uptime < '{dateMax}' ");
            sql.Append(" GROUP BY craftname, ");
            sql.Append("          devicename, ");
            sql.Append("          rfidstationname ");
            sql.Append(" ORDER BY DECODE(craftname, '清洗制绒', 1, '扩散', 2, '激光SE', 3, '刻蚀', 4, '退火', 5, '背钝化', 6, '正镀膜', 7, '背镀膜', 8, '丝网', 9), ");
            sql.Append("          devicename, ");
            sql.Append("          rfidstationname ");
            sql.Append(" ) a ");

            return sql.ToString();

        }
    }
}