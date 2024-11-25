using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;

namespace Project_Assistant_Server.Controllers.API
{
	public class ProjectController : ControllerBase
	{
		DatabaseContext context;
		public ProjectController(DatabaseContext _context)
		{
			context = _context;
		}

		// GET: ProjectController
		public ActionResult Index(String session)
		{
			UserDto userDto = new UserDto();
			String newSession=new Session().verifySession(session);
			if (context.users.Where(a => a.CurrentSession == session).Count() > 0)
			{
				User user = context.users.Where(a => a.CurrentSession == session)
					.Include(a => a.Projects).ThenInclude(a => a.Calendars)
					.Include(a => a.Projects).ThenInclude(a => a.Files)
					.Include(a => a.Projects).ThenInclude(a => a.Logs)
					.Include(a => a.Projects).ThenInclude(a => a.Notes)
					.Include(a => a.Projects).ThenInclude(a => a.Programs)
					.Include(a => a.Projects).ThenInclude(a => a.ToDo).First();
							
				userDto.session = newSession;
				userDto.projects = new List<Project>();
				foreach(Project p in user.Projects)
					userDto.projects.Add(p);
				
				user.CurrentSession = newSession;
				context.users.Update(user);
				context.SaveChanges();
			}

			return Ok(userDto);
		}

	

	

		// POST: ProjectController/Create
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

	
		// POST: ProjectController/Edit/5
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

		// POST: ProjectController/Delete/5
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
