using TWWork_v2.Enums;

namespace TWWork_v2.ViewModels
{
    public class DetailedSiteComparisonViewModel
    {
        public string DateMin { get; set; }
        public string DateMax { get; set; }
        public DeviceEnum DeviceName { get; set; }
        
        public string ProductionLine { get; set; }
        
        public OBTStationNameEnum ObtStation { get; set; }
        
        public RFIDStationNameEnum RfidStation { get; set; }
    }
}