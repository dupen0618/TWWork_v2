using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Reps.IRepository;
using TWWwork.Models;

namespace TWWork_v2.Controllers
{
    public class MissingRecordController : Controller
    {
        private readonly IMissingRecordRepository _repository;
        private List<MissingRecord> _list;
        
        public MissingRecordController(IMissingRecordRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadData()
        {
            _list = _repository.GetMissingRecordTop10();

            var data = new
            {
                code= 0,
                msg = "success",
                count = _list.Count,
                data = _list
            };
            return Json(data);
        }
    }
}