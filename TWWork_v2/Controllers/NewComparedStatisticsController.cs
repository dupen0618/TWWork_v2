using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.ViewModels;
using TWWwork.Models;

namespace TWWork_v2.Controllers
{
    public class NewComparedStatisticsController : Controller
    {
        private readonly IOBTRFIDCountModelRepository _repository;
        private List<OBTRFIDCountModel> _list;

        public NewComparedStatisticsController(IOBTRFIDCountModelRepository repository)
        {
            _repository = repository;
        }

        // GET
        public IActionResult Index(string dateMin, string dateMax)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<OBTRFIDCountModel>();
            }
            else
            {
                _list = _repository.GetOBTRFIDCountModels(dateMin, dateMax);
            }

            NewComparedStatisticsViewModel model = new NewComparedStatisticsViewModel
            {
                DateMin = dateMin,
                DateMax = dateMax
                //DataChangeModels = _list
            };

            return View(model);
        }

        public JsonResult ExportFile(string dateMin, string dateMax)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<OBTRFIDCountModel>();
            }
            else
            {
                _list = _repository.GetOBTRFIDCountModels(dateMin, dateMax);
            }

            return Json(_list);
        }


        public JsonResult LoadData(string dateMin, string dateMax, int page, int limit)
        {
            //string message;
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<OBTRFIDCountModel>();
                //message = "";
            }
            else
            {
                _list = _repository.GetOBTRFIDCountModels(dateMin, dateMax);
            }

            var data = new
            {
                code = 0,
                msg = "hello",
                count = _list.Count,
                data = _list.Skip((page - 1) * limit).Take(limit).ToList()
            };

            return Json(data);
        }
    }
}