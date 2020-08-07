using System.Collections.Generic;
using TWWork_v2.Models;

namespace TWWork_v2.ViewModels
{
    public class ComparedStatisticsViewModel
    {
        public string DateMin { get; set; }

        public string DateMax { get; set; }
        
        public List<OBTRFIDRecordCount> ObtrfidRecordCounts { get; set; }
    }
}