using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TWWork_v2.Dao;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;

namespace TWWork_v2.Reps.Repository
{
    public class RetrospectiveInquiryRepository : IRetrospectiveInquiryRepository
    {
        private readonly AppDbContext _context;
        
        public RetrospectiveInquiryRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<RetrospectiveInquiry> GetRetrospectiveInquiries(
            string dateMin, string dateMax, string device, string pushOrPop)
        {
            List<RetrospectiveInquiry> list = new List<RetrospectiveInquiry>();
            try
            {
                string sql = GetSql(dateMin, dateMax, device, pushOrPop);
                list = _context.RetrospectiveInquiries.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetRetrospectiveInquiries出错:{ex.Message}");
            }

            return list;
        }

        public string GetSql(
            string dateMin, string dateMax, string device, string pushOrPop)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT rownum AS No, ab.* ");
            sql.Append(" FROM ( ");
            sql.Append(" 	SELECT * ");
            sql.Append(" 	FROM ( ");
            sql.Append(" 		SELECT devicename, boxid, rfidstationname, zr.traceid, createdate ");
            sql.Append(" 			, tracerecord.passStationNum AS mypassnum, tracerecord.latetime ");
            sql.Append(" 		FROM ( ");
            sql.Append(" 			SELECT devicename, boxid, rfidstationname, traceid, createdate ");
            sql.Append(" 			FROM dev_tracerecord ");
            sql.Append($" 			WHERE craftname = '{device}' ");
            sql.Append($" 				AND pushorpop = '{pushOrPop}' ");
            sql.Append($" 				AND CREATEDATE > TO_DATE('{dateMin}', 'YY-MM-DD HH24:MI:SS') ");
            sql.Append($" 				AND CREATEDATE < TO_DATE('{dateMax}', 'YY-MM-DD HH24:MI:SS') ");
            sql.Append(" 		) zr ");
            sql.Append(" 			INNER JOIN ( ");
            sql.Append(" 				SELECT traceid, COUNT(*) AS passStationNum, MAX(createdate) AS latetime ");
            sql.Append(" 				FROM dev_tracerecord ");
            sql.Append($" 				WHERE CREATEDATE > TO_DATE('{dateMin}', 'YY-MM-DD HH24:MI:SS') ");
            sql.Append($" 					AND CREATEDATE < TO_DATE('{dateMax}', 'YY-MM-DD HH24:MI:SS') ");
            sql.Append(" 				GROUP BY traceid ");
            sql.Append(" 			) tracerecord ON tracerecord.traceid = zr.traceid  ");
            sql.Append(" 	) A ");
            sql.Append(" 		LEFT JOIN ( ");
            sql.Append(" 			SELECT a.traceid AS mytraceid, a.latetime AS mylatetime, dev_tracerecord.devicename AS mydevicename, dev_tracerecord.pushorpop ");
            sql.Append(" 			FROM ( ");
            sql.Append(" 				SELECT traceid, COUNT(*), MAX(createdate) AS latetime ");
            sql.Append(" 				FROM dev_tracerecord ");
            sql.Append($" 				WHERE CREATEDATE > TO_DATE('{dateMin}', 'YY-MM-DD HH24:MI:SS') ");
            sql.Append($" 					AND CREATEDATE < TO_DATE('{dateMax}', 'YY-MM-DD HH24:MI:SS') ");
            sql.Append(" 				GROUP BY traceid ");
            sql.Append(" 			) a, dev_tracerecord ");
            sql.Append(" 			WHERE a.traceid = dev_tracerecord.traceid ");
            sql.Append(" 				AND a.latetime = dev_tracerecord.createdate ");
            sql.Append(" 		) B ON A.traceid = B.mytraceid  ");
            sql.Append(" ) ab ");

            return sql.ToString();
        }
        
    }
}