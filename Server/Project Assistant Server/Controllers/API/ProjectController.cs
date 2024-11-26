using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;
using System.Linq;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/api/Project")]
	public class ProjectController : ControllerBase
	{
		DatabaseContext context;
		public ProjectController(DatabaseContext _context)
		{
			context = _context;
		}

		// GET: ProjectController
		[HttpGet(Name ="/GetAll/{session}")]
		public ActionResult Index(String session)
		{
			UserDto userDto = new UserDto();
			
			if (context.users.Where(a => a.CurrentSession == session).Any())
			{
				String newSession = new Session(context).newSession(session);
				User user = context.users.Where(a => a.CurrentSession == session)
					.Include(a => a.Projects).ThenInclude(a => a.Calendars)
					.Include(a => a.Projects).ThenInclude(a => a.Files)
					.Include(a => a.Projects).ThenInclude(a => a.Logs)
					.Include(a => a.Projects).ThenInclude(a => a.Notes)
					.Include(a => a.Projects).ThenInclude(a => a.Programs)
					.Include(a => a.Projects).ThenInclude(a => a.ToDo).First();
				
				userDto.session = newSession;
				foreach(Project p in user.Projects)
					userDto.projects.Add(p);

				return Ok(userDto);
			}
			else
			{
				return Unauthorized();
			}
		}

	

	

		// POST: ProjectController/Create
		[HttpPost(Name ="/Create")]
		public ActionResult Create(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.CurrentSession == collection["session"]).Count() > 0)
			{
				
				String sProjectData = collection["ProjectName"];

				if (!context.users.Where(a => a.CurrentSession == collection["session"]).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectData).Any()).Any())
				{
					Project project = new Project();
					project.Name = sProjectData;
					context.projects.Add(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					SessionData sessionData = new SessionData();
					sessionData.session = newSession;
					return Ok(sessionData);
				}
				else
				{
					return BadRequest();
				}				
			}
			else
			{
				return Unauthorized();
			}
		}


		// POST: ProjectController/Edit/5
		[HttpPost(Name = "/Edit")]
		public ActionResult Edit(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.CurrentSession == collection["session"]).Count() > 0)
			{
				String sProjectData = collection["ProjectName"];
				String sProjectId = collection["ProjectId"];

				if (!context.users.Where(a => a.CurrentSession == collection["session"]).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectData && a.Id!=int.Parse(sProjectId)).Any()).Any())
				{
					Project p = context.projects.Where(a=> a.Id == int.Parse(sProjectId)).First();
					p.Name = sProjectData;
					context.projects.Update(p);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					SessionData sessionData = new SessionData();
					sessionData.session = newSession;
					return Ok(sessionData);
				}
				else
				{ 
					return BadRequest(); 
				}
			}
			else
			{
				return Unauthorized();
			}
		}		

		// POST: ProjectController/Delete/5
		[HttpPost(Name = "/Delete")]
		public ActionResult Delete(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.CurrentSession == collection["session"]).Count() > 0)
			{				
				
				String sProjectId = collection["ProjectId"];
				String session = collection["session"];

				if (context.users.Where(a => a.CurrentSession == collection["session"]).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Id == int.Parse(sProjectId)).Any()).Any()) 
				{
					Project project = context.users.Where(a=>a.CurrentSession==session).First().Projects.Where(a => a.Id == int.Parse(sProjectId)).First();
					context.projects.Remove(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					SessionData sessionData = new SessionData();
					sessionData.session = newSession;
					return Ok(sessionData);
				}
				else
				{
					return Unauthorized();
				}
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}
