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
    public class MissingRecordRepository : IMissingRecordRepository
    {
        private readonly AppDbContext _context;

        public MissingRecordRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<MissingRecord> GetMissingRecordTop10()
        {
            List<MissingRecord> list = new List<MissingRecord>();

            try
            {
                string sql = GetSql();
                list = _context.MissingRecords.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetMissingRecordTop10出错:{ex.Message}");
            }

            return list;
        }

        public string GetSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select rownum No,a.* ");
            sql.Append(" from ( select * from DEV_MISSINGRECORD order by createdate desc) a where rownum < 11 ");

            return sql.ToString();
        }
    }
}