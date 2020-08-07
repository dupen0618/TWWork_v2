using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.ViewModels;
using TWWwork.Models;

namespace TWWork_v2.Controllers
{
    public class SuccessRateStatisticsController : Controller
    {
        private readonly ISuccessRateModelRepository _repository;
        private List<SuccessRateModel> _list;
        
        public SuccessRateStatisticsController(ISuccessRateModelRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public IActionResult Index(string dateMin,string dateMax)
        {
            SuccessRateStatisticsViewModel model = new SuccessRateStatisticsViewModel
            {
                DateMin = dateMin,
                DateMax = dateMax
            };
            
            return View(model);
        }

        public JsonResult LoadData(string dateMin,string dateMax,int page,int limit)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<SuccessRateModel>();
            }
            else
            {
                _list = _repository.GetSuccessRateModels(dateMin, dateMax);
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

        public JsonResult ExportFile(string dateMin, string dateMax)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<SuccessRateModel>();
            }
            else
            {
                _list = _repository.GetSuccessRateModels(dateMin, dateMax);
            }

            return Json(_list);
        }
    }
}