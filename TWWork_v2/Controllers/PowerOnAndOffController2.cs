﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWWork_v2.Models;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.ViewModels;

namespace TWWork_v2.Controllers
{
	public class PowerOnAndOffController2 : Controller
	{
		private Idev_TraceRecordRepository _repository;

		public PowerOnAndOffController2(Idev_TraceRecordRepository repository)
		{
			_repository = repository;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult LoadData(string tag, string dateMin, string dateMax, string craftName, string productionLine
			, int page, int limit)
		{
			if (tag == "0")
			{
				var data1 = new
				{
					code = 0,
					msg = "hello",
					count = 0,
					data = new List<dev_TraceRecord>()
				};

				return Json(data1);
			}
			List<dev_TraceRecord> list = null;

			if (string.IsNullOrEmpty(dateMin))
			{
				list = new List<dev_TraceRecord>();
			}
			else
			{
				list = _repository.GetDev_TraceRecords(dateMin, dateMax, $"{productionLine}-{craftName}01");
			}

			var powerOnCount = list.Where(e => !string.IsNullOrEmpty(e.TRACEID) && string.Join("", e.TRACEID.TakeLast(6).ToArray()).StartsWith("A")).Count();
			var powerOffCount = list.Where(e => !string.IsNullOrEmpty(e.TRACEID) && string.Join("", e.TRACEID.TakeLast(6).ToArray()).StartsWith("M")).Count();

			var onOffCount = new
			{
				offCount = powerOffCount,
				onCount = powerOnCount
			};

			var data = new
			{
				code = 0,
				msg = "hello",
				powerOnOffCount = onOffCount,
				count = list.Count,
				data = list.Skip((page - 1) * limit).Take(limit).ToList()
			};

			return Json(data);
		}
	}
}
