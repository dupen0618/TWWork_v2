using System.Collections.Generic;
using TWWwork.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface IRFIDReadCountsRepository
    {
        List<RFIDReadCounts> GetRFIDReadCounts(string dateMin,string dateMax,string deviceName,string stationName);
    }
}