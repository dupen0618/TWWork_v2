using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.ViewModels;
using TWWwork.Models;

namespace TWWork_v2.Controllers
{
    public class NewDataChangeController : Controller
    {
        private readonly IDataChangeModelRepository _repository;

        public NewDataChangeController(IDataChangeModelRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public IActionResult Index(string dateMin,string dateMax)
        {
            NewDataChangeViewModel model = new NewDataChangeViewModel
            {
                DateMin =  dateMin,
                DateMax = dateMax
            };
            return View(model);
        }

        /*
        public JsonResult LoadData(string dateMin,string dateMax)
        {
            List<DataChangeModel> dataChangeModels = _repository.GetDataChangeModels(dateMin, dateMax);
            var dateList = (from model in dataChangeModels
                            group model by model.Time into d
                            orderby d.Key
                            select d.Key);

            List<DataSuccessModel> dataSuccessModels = new List<DataSuccessModel>();

            int i = 1;
            foreach(var date in dateList)
            {
                foreach(var dataChange in dataChangeModels.Where(dc => dc.Time == date))
                {
                    //if(dataSuccessModels.Where(ds => ds.CraftName == dataChange.CraftName && ds.DeviceName == dataChange.DeviceName && ds.RFIDStationName == dataChange.RFIDStationName).Any)
                    if(!dataSuccessModels.Any(ds => ds.CraftName == dataChange.CraftName && ds.DeviceName == dataChange.DeviceName && ds.RFIDStationName == dataChange.RFIDStationName))
                    {
                        dataSuccessModels.Add(new DataSuccessModel
                        { 
                            No = i++,
                            CraftName = dataChange.CraftName,
                            DeviceName = dataChange.DeviceName,
                            RFIDStationName = dataChange.RFIDStationName
                        });
                    }
                }
            }

            foreach (var date in dateList)
            {
                foreach (var dataChange in dataChangeModels.Where(dc => dc.Time == date))
                {
                    DataSuccessModel dataSuccess = dataSuccessModels.Where(ds => ds.CraftName == dataChange.CraftName
                                                                                    && ds.DeviceName == dataChange.DeviceName
                                                                                    && ds.RFIDStationName == dataChange.RFIDStationName).FirstOrDefault();
                    int index = dataSuccessModels.IndexOf(dataSuccess);
                    dataSuccessModels[index].RFIDCntList.Add(dataChange.RFIDCnt);
                    dataSuccessModels[index].MissCntList.Add(dataChange.MissCnt);
                    dataSuccessModels[index].SuccessPercenterList.Add(dataChange.SuccessPercenter);
                }
                for (int j = 0;j < dataSuccessModels.Count();j ++)
                {
                    var ds = dataSuccessModels[j];
                    if(!dataChangeModels.Where(dc => dc.Time == date).Any(dc => dc.CraftName == ds.CraftName && dc.DeviceName == ds.DeviceName && dc.RFIDStationName == ds.RFIDStationName))
                    {
                        dataSuccessModels[j].RFIDCntList.Add("");
                        dataSuccessModels[j].MissCntList.Add("");
                        dataSuccessModels[j].SuccessPercenterList.Add("");
                    }
                }
            }

            
            var data = new
            {
                code = 0,
                msg= "success"
            };

            return Json(data);
        }*/
    }
}