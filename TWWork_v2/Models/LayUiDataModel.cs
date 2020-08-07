using System.Collections.Generic;

namespace TWWork_v2.Models
{
    public class LayUiDataModel
    {
        public string title { get; set; }
        public int id { get; set; }
        public string field { get; set; }
        public bool @checked { get; set; }
        public bool spread { get; set; }
        public List<LayUiDataModel> children { get; set; }
    }
}