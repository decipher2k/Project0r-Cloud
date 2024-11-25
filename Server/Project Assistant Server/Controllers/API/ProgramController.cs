using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project_Assistant_Server.Controllers.API
{
	public class ProgramController : ControllerBase
	{
		
		DatabaseContext context;
		public ProgramController(DatabaseContext _context)
		{
			context = _context;
		}

		// GET: ProgramController
		public ActionResult Index()
		{
			return View();
		}

		// GET: ProgramController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: ProgramController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ProgramController/Create
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

		// GET: ProgramController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: ProgramController/Edit/5
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

		// GET: ProgramController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: ProgramController/Delete/5
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
