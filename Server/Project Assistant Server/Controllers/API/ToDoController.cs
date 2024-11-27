using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/api/ToDo")]
	public class ToDoController : ControllerBase
	{

		DatabaseContext context;
		public ToDoController(DatabaseContext _context)
		{
			context = _context;
		}


		// GET: ProjectController
		[HttpPost("Read")]
		public ActionResult Read(IFormCollection collection)
		{

			String session = collection["session"].ToString();

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
		[HttpPost("Create")]
		public ActionResult Create(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{

				String sProjectData = collection["ItemData"];
				String sProjectName = collection["project"];

				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectData).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a=>a.ToDo).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.ToDo ToDo = JsonConvert.DeserializeObject<ToDo>(sProjectData);
					
					project.ToDo.Add(ToDo);
					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
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
		[HttpPost("Edit")]
		public ActionResult Edit(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{

				String sProjectData = collection["ItemData"];
				String sProjectName = collection["project"];

				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectData).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.ToDo).First()
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

					String newSession = new Session(context).newSession(collection["session"].ToString());
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
		[HttpPost("Delete")]
		public ActionResult Delete(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{

				String sItemId = collection["ItemData"];
				String sProjectName = collection["project"];

				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectName).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.ToDo).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					ToDo todo = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.ToDo).First()
						.Projects.Where(a => a.Name == sProjectName).First().ToDo.Where(a=>a.Id==long.Parse(sItemId)).First();

								
					context.Entry(todo).State = EntityState.Deleted;
					project.ToDo.Remove(project.ToDo.Where(a => a.Id == long.Parse(sItemId)).First());
					context.SaveChanges();
					project.ToDo.Remove(project.ToDo.Where(a => a.Id == long.Parse(sItemId)).First());
					context.Update(project);					
					context.SaveChanges();
						
					String newSession = new Session(context).newSession(collection["session"].ToString());
					IdSessionDto sessionData = new IdSessionDto();
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
	}
}
