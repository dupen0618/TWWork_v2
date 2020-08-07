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
    public class TableInfoRepository :ITableInfoRepository
    {
        private readonly AppDbContext _context;
        
        public TableInfoRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<TableInfo> GetTableSie()
        {
            List<TableInfo> list = new List<TableInfo>();

            try
            {
                string sql = GetSql();
                list = _context.TableInfos.FromSql(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTableSie出错:{ex.Message}");
            }
            return list;
        }

        public string GetSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ue.Segment_Name, ue.tabSize, ut.num_rows ");
            sql.Append(" FROM ( ");
            sql.Append(" 	SELECT Segment_Name, SUM(bytes) / 1024 / 1024 AS tabSize ");
            sql.Append(" 	FROM User_Extents ");
            sql.Append(" 	WHERE Segment_Name LIKE 'DEV%' ");
            sql.Append(" 	GROUP BY Segment_Name ");
            sql.Append(" ) ue ");
            sql.Append(" 	FULL JOIN ( ");
            sql.Append(" 		SELECT t.table_name, t.num_rows ");
            sql.Append(" 		FROM user_tables t ");
            sql.Append(" 		WHERE t.table_name LIKE 'DEV%' ");
            sql.Append(" 	) ut ON ue.Segment_Name = ut.table_name  ");
            sql.Append(" ORDER BY ue.Segment_Name ");

            return sql.ToString();
        }
    }
}