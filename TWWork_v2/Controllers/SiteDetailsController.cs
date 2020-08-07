using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Enums;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.Utils;
using TWWork_v2.ViewModels;
using TWWwork.Models;

namespace TWWork_v2.Controllers
{
    public class SiteDetailsController : Controller
    {
        private readonly IOBTRFIDRecordRepository _repository;
        private List<OBTRecord> _obtList;
        private List<RFIDRecord> _rfidList;
        
        public SiteDetailsController(IOBTRFIDRecordRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public IActionResult Index(string dateMin,string dateMax,
            DeviceEnum deviceName, 
            string productionLine,
            StationCategoryEnum stationCategory, 
            RFIDStationNameEnum rfidStation)
        {
            SiteDetailsViewModel model = new SiteDetailsViewModel
            {
                DateMin = dateMin,
                DateMax = dateMax,
                DeviceName = deviceName,
                ProductionLine = productionLine,
                StationCategory = stationCategory,
                RfidStationName = rfidStation
            };
            
            return View(model);
        }

        public JsonResult LoadData(string dateMin,
            string dateMax,
            DeviceEnum deviceName, 
            string productionLine,
            StationCategoryEnum stationCategory, 
            RFIDStationNameEnum rfidStation,
            int page,int limit)
        {
            dynamic list = new List<object>();
            int cnt = 0;
            
            if (string.IsNullOrEmpty(dateMin))
            {
                _obtList = new List<OBTRecord>();
                _rfidList = new List<RFIDRecord>();
                //list = _obtList;
            }
            else
            {
                string device = $"{productionLine}-{deviceName}";
                string station = $"{rfidStation}";
                //_list = _repository.GetObtOrRfidRecords(dateMin, dateMax, device, stationCategory, station);
                if (stationCategory == StationCategoryEnum.OBT)
                {
                    _obtList = _repository.GetObtRecords(dateMin, dateMax, device, station);
                    cnt = _obtList.Count;
                    list = _obtList.Skip((page - 1) * limit).Take(limit).ToList();
                }
                else if (stationCategory == StationCategoryEnum.RFID)
                {
                    _rfidList = _repository.GetRfidRecords(dateMin, dateMax, device, station);
                    cnt = _rfidList.Count;
                    list = _rfidList.Skip((page - 1) * limit).Take(limit).ToList();
                }
            }
            
            var data= new
            {
                code = 0,
                msg = "hello",
                count = cnt,
                category = stationCategory,
                data = list
            }; 
            
            return Json(data);
        }

        public JsonResult ExportFile(string dateMin,
            string dateMax,
            DeviceEnum deviceName,
            string productionLine,
            StationCategoryEnum stationCategory,
            RFIDStationNameEnum rfidStation)
        {
            dynamic list = new List<object>();
            //int cnt = 0;
            
            if (string.IsNullOrEmpty(dateMin))
            {
                _obtList = new List<OBTRecord>();
                _rfidList = new List<RFIDRecord>();
                //list = _obtList;
            }
            else
            {
                string device = $"{productionLine}-{deviceName}";
                string station = $"{rfidStation}";
                //_list = _repository.GetObtOrRfidRecords(dateMin, dateMax, device, stationCategory, station);
                if (stationCategory == StationCategoryEnum.OBT)
                {
                    _obtList = _repository.GetObtRecords(dateMin, dateMax, device, station);
                    list = _obtList;
                }
                else if (stationCategory == StationCategoryEnum.RFID)
                {
                    _rfidList = _repository.GetRfidRecords(dateMin, dateMax, device, station);
                    list = _rfidList;
                }
            }

            return Json(list);
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