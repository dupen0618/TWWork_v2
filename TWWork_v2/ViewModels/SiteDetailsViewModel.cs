using System.Collections.Generic;
using TWWork_v2.Enums;

namespace TWWork_v2.ViewModels
{
    public class SiteDetailsViewModel
    {
        public string DateMin { get; set; }
        public string DateMax { get; set; }
        public DeviceEnum DeviceName { get; set; }
        public string ProductionLine { get; set; }
        public StationCategoryEnum StationCategory { get; set; }
        public RFIDStationNameEnum RfidStationName { get; set; }
    }
}