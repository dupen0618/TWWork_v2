using System.Collections.Generic;
using TWWwork.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface IDeviceWorkRecordRepository
    {
        List<DeviceWorkRecord> GetDeviceWorkRecords();

        int AddDeviceWorkRecord(DeviceWorkRecord model);

        int DelDeviceWorkRecord(int id);

        int EditDeviceWorkRecord(DeviceWorkRecord model);
    }
}