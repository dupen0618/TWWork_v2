using System.ComponentModel.DataAnnotations;
using TWWork_v2.Enums;

namespace TWWork_v2.ViewModels
{
    public class RFIDReadCountsViewModel : BaseViewModel
    {
        public DeviceEnum DeviceName { get; set; }

        [Display(Name = "产线")]
        public string ProductionLine { get; set; }

        [Display(Name = "站点")]
        public RFIDStationNameEnum StationName { get; set; }
    }
}