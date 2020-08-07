using TWWork_v2.Enums;

namespace TWWork_v2.ViewModels
{
    public class DeviceWorkRecordViewModel
    {
        public int ID { get; set; }
        public DeviceEnum DeviceName { get; set; }
        public string ProductionLine { get; set; }
        public OBTStationNameEnum StationName { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string SubmitPerson { get; set; }
    }
}