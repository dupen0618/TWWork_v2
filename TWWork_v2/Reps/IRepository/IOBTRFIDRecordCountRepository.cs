using System.Collections.Generic;
using TWWork_v2.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface IOBTRFIDRecordCountRepository
    {
        List<OBTRFIDRecordCount> GetOBTRFIDRecordCounts(string dateMin, string dateMax);
    }
}