using System.Collections.Generic;
using TWWork_v2.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface ITableInfoRepository
    {
        List<TableInfo> GetTableSie();
    }
}