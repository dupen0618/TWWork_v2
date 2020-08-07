using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.ViewModels;

namespace TWWork_v2.Controllers
{
    public class ComparedStatisticsController : Controller
    {
        
        private readonly IOBTRFIDRecordCountRepository _repository;
        private List<OBTRFIDRecordCount> _list;

        public ComparedStatisticsController(IOBTRFIDRecordCountRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public IActionResult Index()
        {
            DateTime dt = DateTime.Now;
            
            ComparedStatisticsViewModel model = new ComparedStatisticsViewModel
            {
                // DateMin = string.Format("{0:yyyy-MM-dd} 00:00:00",dt),
                // DateMax = string.Format("{0:yyyy-MM-dd} 23:59:59",dt)
            };
            //return RedirectToAction("IndexNoLayout");
            return View(model);
        }

        public IActionResult IndexNoLayout(string dateMin,string dateMax)
        {
            
            ComparedStatisticsViewModel model = new ComparedStatisticsViewModel
            {
                DateMin = dateMin,
                DateMax = dateMax,
                //ObtrfidRecordCounts = _list
            };
            return View(model);
        }
        
        
        public JsonResult LoadData(string dateMin,string dateMax ,int page, int limit)
        {
            
            if (dateMin == null || dateMin == "")
            {
                _list = new List<OBTRFIDRecordCount>();
            }
            else
            {
                _list = _repository.GetOBTRFIDRecordCounts(dateMin, dateMax);
            }

            var data = new
            {
                code = 0,
                msg = "hello",
                count = _list.Count,
                data = _list.Skip(page).Take(limit).ToList()
            };

            return Json(data);
        }

        public JsonResult ExportFile(string dateMin, string dateMax)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<OBTRFIDRecordCount>();
            }
            else
            {
                _list = _repository.GetOBTRFIDRecordCounts(dateMin, dateMax);
            }
            
            return Json(_list);
        }
    }
}