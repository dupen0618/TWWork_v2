using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Dao;
using TWWork_v2.Enums;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.Utils;
using TWWork_v2.ViewModels;
using TWWwork.Models;

namespace TWWork_v2.Controllers
{
    public class DetailedSiteComparisonController : Controller
    {
        private readonly IOBTRFIDRecordRepository _repository;
        private List<OBTRFIDRecord> _list;

        public DetailedSiteComparisonController(IOBTRFIDRecordRepository repository)
        {
            _repository = repository;
        }

        // GET
        public IActionResult Index(string dateMin,string dateMax
            , DeviceEnum deviceName, string productionLine
            , OBTStationNameEnum obtStation, RFIDStationNameEnum rfidStation)
        {
            DetailedSiteComparisonViewModel model = new DetailedSiteComparisonViewModel()
            {
                DateMin = dateMin,
                DateMax = dateMax,
                DeviceName = deviceName,
                ProductionLine = productionLine,
                ObtStation =  obtStation,
                RfidStation = rfidStation
            };
            
            return View(model);
        }

        /// <summary>
        /// 动态加载数据
        /// </summary>
        /// <param name="dateMin"></param>
        /// <param name="dateMax"></param>
        /// <param name="deviceName"></param>
        /// <param name="productionLine"></param>
        /// <param name="obtStation"></param>
        /// <param name="rfidStation"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public JsonResult LoadData(string dateMin,string dateMax
            , DeviceEnum deviceName, string productionLine
            , OBTStationNameEnum obtStation, RFIDStationNameEnum rfidStation
            , int page,int limit)
        {
            
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<OBTRFIDRecord>();
            }
            else
            {
                string device = $"{productionLine}-{deviceName}";
                string obt = $"{obtStation}";
                string rfid = $"{rfidStation}";
                _list = _repository.GetOBTRFIDRecords(dateMin, dateMax, device,obt,rfid);
            }

            var data = new
            {
                code = 0,
                msg = "hello",
                count = _list.Count,
                data = _list.Skip((page - 1)* limit).Take(limit).ToList()
            };
            
            return Json(data);
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="dateMin"></param>
        /// <param name="dateMax"></param>
        /// <param name="deviceName"></param>
        /// <param name="productionLine"></param>
        /// <param name="obtStation"></param>
        /// <param name="rfidStation"></param>
        /// <returns></returns>
        public JsonResult ExportFile(string dateMin,string dateMax
            , DeviceEnum deviceName, string productionLine
            , OBTStationNameEnum obtStation, RFIDStationNameEnum rfidStation)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<OBTRFIDRecord>();
            }
            else
            {
                string device = $"{productionLine}-{deviceName}";
                string obt = $"{obtStation}";
                string rfid = $"{rfidStation}";
                _list = _repository.GetOBTRFIDRecords(dateMin, dateMax, device,obt,rfid);
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