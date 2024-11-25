using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project_Assistant_Server.Controllers.API
{
	public class CalendarController : ControllerBase
	{

		DatabaseContext context;
		public CalendarController(DatabaseContext _context) { 
			context = _context;
		}

		// GET: CalendarController
		public ActionResult Index()
		{
			return View();
		}

		// GET: CalendarController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: CalendarController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CalendarController/Create
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

		// GET: CalendarController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CalendarController/Edit/5
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

		// GET: CalendarController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CalendarController/Delete/5
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
