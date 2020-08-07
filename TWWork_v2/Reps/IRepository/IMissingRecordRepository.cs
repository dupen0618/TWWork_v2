using System.Collections.Generic;
using TWWwork.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface IMissingRecordRepository
    {
        List<MissingRecord> GetMissingRecordTop10();
    }
}