using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWWork_v2.Models
{
    public class dev_MissingRecord
    {
        public string CraftName { get; set; }
        public string DeviceName { get; set; }
        public string RFIDStationName{ get; set; }
        public string MissingCount { get; set; }
    }
}
