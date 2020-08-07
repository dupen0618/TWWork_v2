using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Enums;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.Utils;
using TWWork_v2.ViewModels;
using TWWwork.Models;

namespace TWWork_v2.Controllers
{
    public class InOutCompareDataController : Controller
    {
        private readonly IInOutCompareDataModelRepository _repository;
        private List<InOutCompareDataModel> _list;

        public InOutCompareDataController(IInOutCompareDataModelRepository repository)
        {
            _repository = repository;
        }

        // GET
        public IActionResult Index(string dateMin,
            string dateMax,
            DeviceEnum deviceName,
            string productionLine)
        {
            InOutCompareDataViewModel model = new InOutCompareDataViewModel
            {
                DateMin = dateMin,
                DateMax = dateMax,
                DeviceName = deviceName,
                ProductionLine = productionLine
            };

            return View(model);
        }

        public JsonResult LoadData(string dateMin,
            string dateMax,
            DeviceEnum deviceName,
            string productionLine,
            int page, int limit)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<InOutCompareDataModel>();
            }
            else
            {
                string device = $"{productionLine}-{deviceName}";
                _list = _repository.GetInOutCompareDataModels(dateMin, dateMax, device);
            }
            
            var data = new
            {
                code = 0,
                msg = "success",
                count = _list.Count,
                data = _list.Skip((page - 1) * limit).Take(limit).ToList()
            };

            return Json(data);
        }


        public JsonResult ExportFile(string dateMin,
            string dateMax,
            DeviceEnum deviceName,
            string productionLine)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<InOutCompareDataModel>();
            }
            else
            {
                string device = $"{productionLine}-{deviceName}";
                _list = _repository.GetInOutCompareDataModels(dateMin, dateMax, device);
            }

            return Json(_list);
        }


        public JsonResult LoadProductionLineInfo(string deviceName)
        {
            List<string> productionLineList = new List<string>();

            if (!string.IsNullOrEmpty(deviceName))
            {
                switch (deviceName)
                {
                    case "1":
                        productionLineList = DataProcess.SetProductionLine(9);
                        break;
                    case "2":
                        productionLineList = DataProcess.SetProductionLine(6);
                        break;
                    case "3":
                        productionLineList = DataProcess.SetProductionLine(9);
                        break;
                    case "4":
                        productionLineList = DataProcess.SetProductionLine(12);
                        break;
                    case "5":
                        productionLineList = DataProcess.SetProductionLine(6);
                        break;
                    case "6":
                        productionLineList = DataProcess.SetProductionLine(7);
                        break;
                    case "7":
                        productionLineList = DataProcess.SetProductionLine(15);
                        break;
                    case "8":
                        productionLineList = DataProcess.SetProductionLine(16);
                        break;
                    case "9":
                        productionLineList = DataProcess.SetProductionLine(8);
                        break;
                    default:
                        break;
                }
            }

            return Json(productionLineList);
        }
    }
}