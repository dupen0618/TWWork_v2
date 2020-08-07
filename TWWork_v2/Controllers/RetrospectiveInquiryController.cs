using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Enums;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.Utils;
using TWWork_v2.ViewModels;

namespace TWWork_v2.Controllers
{
    public class RetrospectiveInquiryController : Controller
    {
        private readonly IRetrospectiveInquiryRepository _repository;
        private List<RetrospectiveInquiry> _list;

        public RetrospectiveInquiryController(IRetrospectiveInquiryRepository repository)
        {
            _repository = repository;
        }

        // GET
        public IActionResult Index(string dateMin, string dateMax,
            DeviceEnum deviceName,PushOrPopEnum pushOrPop)
        {
            RetrospectiveInquiryViewModel model = new RetrospectiveInquiryViewModel
            {
                DateMin = dateMin,
                DateMax = dateMax,
                PushOrPop = pushOrPop,
                DeviceName = deviceName
            };
            return View(model);
        }

        public JsonResult LoadData(string dateMin, string dateMax,
            DeviceEnum deviceName,PushOrPopEnum pushOrPop,
            int limit,int page)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<RetrospectiveInquiry>();
            }
            else
            {
                string device = DataProcess.GetEnumName(deviceName);
                string pushPop = $"{pushOrPop}";
                _list = _repository.GetRetrospectiveInquiries(dateMin, dateMax, device, pushPop);
            }
            
            var data = new
            {
                code = 0,
                msg = "success",
                count = _list.Count,
                data = _list.Skip((page - 1) * limit).Take(limit)
            };

            return Json(data);
        }

        public JsonResult ExportFile(string dateMin, string dateMax,
            DeviceEnum deviceName, PushOrPopEnum pushOrPop)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<RetrospectiveInquiry>();
            }
            else
            {
                string device = DataProcess.GetEnumName(deviceName);
                string pushPop = $"{pushOrPop}";
                _list = _repository.GetRetrospectiveInquiries(dateMin, dateMax, device, pushPop);
            }

            return Json(_list);
        }
        
    }
}