using System.Collections.Generic;
using TWWwork.Models;

namespace TWWork_v2.ViewModels
{
    public class NewComparedStatisticsViewModel
    {
        public string DateMin { get; set; }

        public string DateMax { get; set; }
        
        public List<DataChangeModel> DataChangeModels { get; set; }
    }
}