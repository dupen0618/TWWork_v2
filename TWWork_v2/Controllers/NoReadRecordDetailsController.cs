using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.ViewModels;

namespace TWWork_v2.Controllers
{
	public class NoReadRecordDetailsController : Controller
	{
		private Idev_MissingRecordRepository _repository;

		public NoReadRecordDetailsController(Idev_MissingRecordRepository repository)
		{
			_repository = repository;
		}

		public IActionResult Index()
		{
			return View();
		}

		public JsonResult LoadData(string dateMin, string dateMax, int isExprot)
		{
			List<dev_MissingRecord> list = null;
			if (string.IsNullOrEmpty(dateMin) || string.IsNullOrEmpty(dateMax))
				list = new List<dev_MissingRecord>();
			else
				list = _repository.PageList(dateMin, dateMax);

			if (isExprot == 1)
			{
				WriteToTxt(list);
			}

			var data = new
			{
				code = 0,
				msg = "hello",
				count = list == null ? 0 : list.Count,
				data = list
			};

			return Json(data);
		}

		private void WriteToTxt(List<dev_MissingRecord> list)
		{
			var now = DateTime.Now;
			string exportExcelPath = $@"E:\TWWork_v2\{now.ToString("MMdd")}\{now.ToString("yyyyMMdd")}.txt"; ;

			FileStream fs = new FileStream(exportExcelPath, FileMode.Create, FileAccess.ReadWrite);
			//fs.Lock(0, fs.Length);
			StreamWriter sw = new StreamWriter(fs);
			var craftNames = list.GroupBy(m => m.CraftName).Select(m => m.Key).ToList();

			foreach (var craftName in craftNames)
			{
				sw.WriteLine($"{craftName}:");
				var deviceNames = list.Where(m => m.CraftName == craftName).GroupBy(m => m.DeviceName).Select(m => m.Key).ToList();
				foreach (var deviceName in deviceNames)
				{
					var ls = list.Where(m => m.DeviceName == deviceName && m.CraftName == craftName).ToList();
					StringBuilder buffer = new StringBuilder();
					buffer.Append($"{deviceName}#");
					for (int i = 0; i < ls.Count; i++)
					{
						if (i == ls.Count - 1)
						{

							buffer.Append($"{ls[i].RFIDStationName}({ls[i].MissingCount})");
						}
						else
							buffer.Append($"{ls[i].RFIDStationName}({ls[i].MissingCount})、");
					}
					sw.WriteLine(buffer.ToString());
				}
			}
			//fs.Unlock(0, fs.Length);//一定要用在Flush()方法以前，否则抛出异常。
			sw.Flush();
			fs.Close();
		}
	}
}
