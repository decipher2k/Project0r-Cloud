using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;

namespace Project_Assistant_Server.Controllers.API
{
	public class ToDoController : ControllerBase
	{

		DatabaseContext context;
		public ToDoController(DatabaseContext _context)
		{
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

				Models.ToDo ToDo = context.users.Where(a => a.CurrentSession == session)
					.Include(a => a.Projects).ThenInclude(a => a.ToDo).First()
					.Projects.Where(a => a.Name == project).First()
					.ToDo.Where(a => a.Id == iItemId).First();

				return Ok(ToDo);
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
				String sProjectName = collection["project"];

				if (!context.users.Where(a => a.CurrentSession == collection["session"]).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectData).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"]).
						Include(a => a.Projects).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.ToDo ToDo = JsonConvert.DeserializeObject<ToDo>(sProjectData);
					project.ToDo.Add(ToDo);
					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = ToDo.Id;
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

					Models.ToDo ToDo = JsonConvert.DeserializeObject<Models.ToDo>(sProjectData);
					for (int i = 0; i < project.ToDo.Count; i++)
					{
						if (project.ToDo[i].Id == ToDo.Id)
						{
							project.ToDo[i] = ToDo;
							break;
						}
					}

					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = ToDo.Id;
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

					Models.ToDo ToDo = JsonConvert.DeserializeObject<Models.ToDo>(sProjectData);
					for (int i = 0; i < project.ToDo.Count; i++)
					{
						if (project.ToDo[i].Id == ToDo.Id)
						{
							project.ToDo.RemoveAt(i);
							break;
						}
					}

					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = ToDo.Id;
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
