using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.ViewModels;

namespace TWWork_v2.Controllers
{
	public class PowerOnAndOffController : Controller
	{
		private Idev_TraceRecordRepository _repository;

		public PowerOnAndOffController(Idev_TraceRecordRepository repository)
		{
			_repository = repository;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult LoadData(string dateMin, string dateMax)
		{

			List<DevTraceRecord> list = _repository.FindDevTraceRecordsByDate(dateMin, dateMax);
			List<TraceRecordPercent> options = new List<TraceRecordPercent>();

			if (list != null && list.Count > 0)
			{
				var deviceNames = list.Select(e => e.DeviceName).Distinct();

				foreach (var name in deviceNames)
				{
					var inTraceRecord = list.FirstOrDefault(e => e.DeviceName == name && e.PushOrPop == "IN")?? new DevTraceRecord();
					var outTraceRecord = list.FirstOrDefault(e => e.DeviceName == name && e.PushOrPop == "OUT")?? new DevTraceRecord();
					string[] names = name.Split("-");
					string result = NameConversion(names[2]);
					int siteNo = int.Parse(names[1]);

					double inACount = inTraceRecord.traceCordACount ?? 0;
					double inMCount = inTraceRecord.traceCordMCount ?? 0;
					double outACount = outTraceRecord.traceCordACount ?? 0;
					double outMCount = outTraceRecord.traceCordMCount ?? 0;


					if (options.Where(e => e.Name == result).Count() > 0)
					{
						var dev = options.FirstOrDefault(e => e.Name == result);
						int index = options.IndexOf(dev);
						options[index].Items.Add(new Item
						{
							SiteNo = siteNo,
							InPowerOnPercent = (inACount / (inACount + inMCount)).ToString("P"),
							OutPowerOnPercent = (outACount / (outACount + outMCount)).ToString("P")
						});
					}
					else
					{

						TraceRecordPercent traceRecordPercent = new TraceRecordPercent();
						traceRecordPercent.Name = result;
						Item item = new Item{
								SiteNo = siteNo,
								InPowerOnPercent = (inACount / (inACount + inMCount)).ToString("P"),
								OutPowerOnPercent = (outACount / (outACount + outMCount)).ToString("P")
							};
						traceRecordPercent.Items.Add(item);
						options.Add(traceRecordPercent);
					}
				}
			}

			options = options.OrderBy(o =>{
				int i = Array.IndexOf(new string[]{"清洗制绒","扩散","激光SE","刻蚀","退火","背钝化","正镀膜","背镀膜","丝网"},o.Name);
				if(i != -1){
					return i;
				}
				else{
					return int.MaxValue;
				}
			}).ToList();

			List<TraceRecordPercent> newOptions = new List<TraceRecordPercent>();
			foreach(var option in options)
			{
				TraceRecordPercent tr = new TraceRecordPercent();
				tr.Name = option.Name;
				tr.Items = option.Items.OrderBy(i => i.SiteNo).ToList();
				newOptions.Add(tr);
			}

			string jsonStr = JsonConvert.SerializeObject(newOptions);
			return Json(jsonStr);
		}

		public string NameConversion(string name)
		{
			string result = "";
			switch (name)
			{
				case "ZR01":
					result = "清洗制绒";
					break;
				case "SJ01":
					result = "丝网";
					break;
				case "BM01":
					result = "背镀膜";
					break;
				case "ZM01":
					result = "正镀膜";
					break;
				case "KS01":
					result = "扩散";
					break;
				case "KES01":
					result = "刻蚀";
					break;
				case "SE01":
					result = "激光SE";
					break;
				case "TH01":
					result = "退火";
					break;
				case "WD01":
					result = "背钝化";
					break;
			}
			return result;
		}
	}
}
