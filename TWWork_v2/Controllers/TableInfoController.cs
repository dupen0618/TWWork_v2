using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;

namespace TWWork_v2.Controllers
{
    public class TableInfoController : Controller
    {
        private readonly ITableInfoRepository _repository;
        private List<TableInfo> _list;
        
        public TableInfoController(ITableInfoRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public IActionResult Index()
        {
            _list = new List<TableInfo>();
            return View();
        }

        public JsonResult LoadData(int page, int limit)
        {
            _list = _repository.GetTableSie();
            
            var data = new
            {
                code = 0,
                msg="success",
                count = _list.Count,
                data = _list.Skip((page -1) * limit).Take(limit).ToList()
            };
            return Json(data);
        }
        
    }
}