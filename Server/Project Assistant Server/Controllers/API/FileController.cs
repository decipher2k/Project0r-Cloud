using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_Assistant_Server.Models;
using Project_Assistant_Server.Dto;

namespace Project_Assistant_Server.Controllers.API
{
	public class FileController : ControllerBase
	{


		DatabaseContext context;
		public FileController(DatabaseContext _context)
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

				Models.File File = context.users.Where(a => a.CurrentSession == session)
					.Include(a => a.Projects).ThenInclude(a => a.Files).First()
					.Projects.Where(a => a.Name == project).First()
					.Files.Where(a => a.Id == iItemId).First();

				return Ok(File);
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

					Models.File File = JsonConvert.DeserializeObject<Models.File>(sProjectData);
					project.Files.Add(File);
					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = File.Id;
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

					Models.File File = JsonConvert.DeserializeObject<Models.File>(sProjectData);
					for (int i = 0; i < project.Files.Count; i++)
					{
						if (project.Files[i].Id == File.Id)
						{
							project.Files[i] = File;
							break;
						}
					}

					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = File.Id;
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

					Models.File File = JsonConvert.DeserializeObject<Models.File>(sProjectData);
					for (int i = 0; i < project.Files.Count; i++)
					{
						if (project.Files[i].Id == File.Id)
						{
							project.Files.RemoveAt(i);
							break;
						}
					}

					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"]);
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = File.Id;
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
