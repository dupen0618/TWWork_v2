using System.Collections.Generic;
using TWWork_v2.Enums;
using TWWork_v2.Models;
using TWWwork.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface IOBTRFIDRecordRepository
    {
        List<OBTRFIDRecord> GetOBTRFIDRecords(string dateMin,string dateMax,string deviceName,string obtStationName, string rFIDStationName);

        List<OBTRFIDRecord> GetObtOrRfidRecords(string dateMin, string dateMax, string deviceName, StationCategoryEnum stationCategory, string rfidStation);
        List<OBTRecord> GetObtRecords(string dateMin, string dateMax, string deviceName, string rfidStation);
        List<RFIDRecord> GetRfidRecords(string dateMin, string dateMax, string deviceName, string rfidStation);
    }
}