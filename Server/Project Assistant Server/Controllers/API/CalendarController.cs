using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;

namespace Project_Assistant_Server.Controllers.API
{
	public class CalendarController : ControllerBase
	{

		DatabaseContext context;
		public CalendarController(DatabaseContext _context) { 
			context = _context;
		}

		// GET: ProjectController
		[HttpPost(Name = "/Read")]
		public ActionResult Read(IFormCollection collection)
		{
			
			String session = collection["session"];

			if (context.users.Where(a => a.CurrentSession == session).Any())
			{
				
				int iItemId = int.Parse(collection["ItemId"]);
				String project = collection["project"];

				Calendar calendar=context.users.Where(a => a.CurrentSession == session)
					.Include(a => a.Projects).ThenInclude(a => a.Calendars).First()
					.Projects.Where(a=>a.Name==project).First()
					.Calendars.Where(a=>a.Id==iItemId).First();		
				
				return Ok(calendar);				
			}
			else
			{
				return Unauthorized();
			}
		}





		// POST: ProjectController/Create
		[HttpPost(Name = "/Create")]
		public ActionResult Create(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.CurrentSession == collection["session"]).Count() > 0)
			{

				String sProjectData = collection["ItemData"];
				String sProjectName=collection["project"];

				if (!context.users.Where(a => a.CurrentSession == collection["session"]).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectData).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"]).
						Include(a => a.Projects).First()
						.Projects.Where(a=>a.Name== sProjectName).First();

					Calendar calendar = JsonConvert.DeserializeObject<Calendar>(sProjectData);
					project.Calendars.Add(calendar);
					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = calendar.Id;
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

				String sProjectData = collection["ItemData"];
				String sProjectName = collection["project"];

				if (!context.users.Where(a => a.CurrentSession == collection["session"]).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectData).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"]).
						Include(a => a.Projects).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Calendar calendar = JsonConvert.DeserializeObject<Calendar>(sProjectData);
					for (int i = 0; i < project.Calendars.Count; i++)
					{
						if (project.Calendars[i].Id == calendar.Id)
						{
							project.Calendars[i] = calendar;
							break;
						}
					}

					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = calendar.Id;
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

				String sProjectData = collection["ItemData"];
				String sProjectName = collection["project"];

				if (!context.users.Where(a => a.CurrentSession == collection["session"]).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectData).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"]).
						Include(a => a.Projects).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Calendar calendar = JsonConvert.DeserializeObject<Calendar>(sProjectData);
					for (int i = 0; i < project.Calendars.Count; i++)
					{
						if (project.Calendars[i].Id == calendar.Id)
						{
							project.Calendars.RemoveAt(i);							
							break;
						}
					}

					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = calendar.Id;
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
	}
}
