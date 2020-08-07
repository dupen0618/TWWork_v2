using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Enums;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.Utils;
using TWWork_v2.ViewModels;
using TWWwork.Models;

namespace TWWork_v2.Controllers
{
    public class DeviceWorkRecordController : Controller
    {
        private readonly IDeviceWorkRecordRepository _repository;
        private List<DeviceWorkRecord> _list;

        public DeviceWorkRecordController(IDeviceWorkRecordRepository repository)
        {
            _repository = repository;
        }

        // GET
        public IActionResult Index()
        {
            if (_list == null)
            {
                _list = new List<DeviceWorkRecord>();
            }

            return View();
        }

        public IActionResult Create()
        {
            DeviceWorkRecordViewModel model = new DeviceWorkRecordViewModel
            {
                DeviceName = DeviceEnum.Default,
                ProductionLine = "",
                StationName = OBTStationNameEnum.Default,
                Description = "",
                CreateDate = "",
                SubmitPerson = ""
            };

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var record = _repository.GetDeviceWorkRecords()
                .Where(item => item.Id == id).First();
            
            DeviceWorkRecordViewModel model = new DeviceWorkRecordViewModel
            {
                ID = record.Id,
                DeviceName = DataProcess.GetDviceEnum(record.DeviceName),
                ProductionLine = record.CraftName,
                StationName = DataProcess.GetStationEmun(record.StationName),
                Description = record.Description,
                CreateDate = record.CreateDate,
                SubmitPerson = record.SubmitPerson
            };

            return View(model);
        }

        public JsonResult LoadData(int page, int limit)
        {
            _list = _repository.GetDeviceWorkRecords();

            var data = new
            {
                code = 0,
                msg = "success",
                count = _list.Count,
                data = _list.Skip((page - 1) * limit).Take(limit).ToList()
            };
            return Json(data);
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

        public JsonResult AddDeviceWorkRecord(DeviceEnum deviceName,
            string productionLine,
            OBTStationNameEnum stationName,
            string description,
            string createDate,
            string submitPerson)
        {

            string device = DataProcess.GetEnumName(deviceName);
            string station = DataProcess.GetEnumName(stationName);

            int maxId =_repository.GetDeviceWorkRecords().Max(item => item.Id);
            
            DeviceWorkRecord  record = new DeviceWorkRecord
            {
                Id = maxId + 1,
                DeviceName = device,
                CraftName = productionLine,
                StationName = station,
                Description = description,
                CreateDate = createDate,
                SubmitPerson = submitPerson
            };

            int result = _repository.AddDeviceWorkRecord(record);
            int code = 0;
            if (result > 0)
            {
                code = 0;
            }
            else
            {
                code = 1;
            }
            
            var json = new
            {
                code = code
            };

            return Json(json);
        }

        public JsonResult delDeviceWorkRecord(int id)
        {
            int result = _repository.DelDeviceWorkRecord(id);

            var data = new
            {
                code = result
            };

            return Json(data);
        }
        
        public JsonResult EditDeviceWorkRecord(int id,
            DeviceEnum deviceName,
            string productionLine,
            OBTStationNameEnum stationName,
            string description,
            string createDate,
            string submitPerson)
        {

            string device = DataProcess.GetEnumName(deviceName);
            string station = DataProcess.GetEnumName(stationName);
            
            DeviceWorkRecord model = new DeviceWorkRecord
            {
                Id = id,
                DeviceName = device,
                CraftName = createDate,
                StationName = station,
                Description = description,
                CreateDate = createDate,
                SubmitPerson = submitPerson
            };

            int result = _repository.EditDeviceWorkRecord(model);
            
            int code = 0;
            if (result > 0)
            {
                code = 0;
            }
            else
            {
                code = 1;
            }
            
            var json = new
            {
                code = code
            };

            return Json(json);
        }
    }
}