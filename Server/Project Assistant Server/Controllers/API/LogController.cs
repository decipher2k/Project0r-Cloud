﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project_Assistant_Server.Controllers.API
{
	public class LogController : ControllerBase
	{

		DatabaseContext context;
		public LogController(DatabaseContext _context)
		{
			context = _context;
		}
		// GET: LogController
		public ActionResult Index()
		{
			return View();
		}

		// GET: LogController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: LogController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: LogController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: LogController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: LogController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: LogController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: LogController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
