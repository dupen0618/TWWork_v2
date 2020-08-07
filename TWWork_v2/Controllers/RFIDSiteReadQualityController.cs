using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.ViewModels;
using TWWwork.Models;

namespace TWWork_v2.Controllers
{
    public class RFIDSiteReadQualityController : Controller
    {
        private readonly ISiteReadQualityRepository _repository;
        private List<SiteReadQuality> _list;

        public RFIDSiteReadQualityController(ISiteReadQualityRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public IActionResult Index(string dateMin,string dateMax)
        {
            RFIDSiteReadQualityViewModel model = new RFIDSiteReadQualityViewModel
            {
                DateMin = dateMin,
                DateMax = dateMax
            };
            return View(model);
        }

        public JsonResult LoadData(string dateMin, 
            string dateMax, int page, int limit)
        {
            if (string.IsNullOrEmpty(dateMin))
            {
                _list = new List<SiteReadQuality>();
            }
            else
            {
                _list = _repository.GetSiteReadQualitys(dateMin, dateMax);
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
                _list = new List<SiteReadQuality>();
            }
            else
            {
                _list = _repository.GetSiteReadQualitys(dateMin, dateMax);
            }

            return Json(_list);
        }
    }
}