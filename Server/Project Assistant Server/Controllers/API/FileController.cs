using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_Assistant_Server.Models;
using Project_Assistant_Server.Dto;
using System.Globalization;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/api/File")]
	public class FileController : ControllerBase
	{


		DatabaseContext context;
		public FileController(DatabaseContext _context)
		{
			context = _context;
		}

		// GET: ProjectController
		[HttpPost("Read")]
		public ActionResult Read(IFormCollection collection)
		{

			String session = collection["session"].ToString();

			if (context.users.Where(a => a.CurrentSession == session && a.IsAdmin==false).Any())
			{

				int iItemId = int.Parse(collection["ItemId"]);
				String project = collection["project"];

				Models.File File = context.users.Where(a => a.CurrentSession == session && a.IsAdmin==false)
					.Include(a => a.Projects).ThenInclude(a => a.Files).First()
					.Projects.Where(a => a.Name == project).First()
					.Files.Where(a => a.Id == iItemId).First();

				String calendarStr = JsonConvert.SerializeObject(File);
				ItemDto item = new ItemDto();
				item.session = new Session(context).newSession(session);
				item.item = calendarStr;
				return Ok(item);
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

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{

				String sProjectData = collection["ItemData"];
				String sProjectName = collection["project"];

				if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectName).Any()).Any())
				{
					Project project = context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.Files).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.File File = JsonConvert.DeserializeObject<Models.File>(sProjectData);
					project.Files.Add(File);
					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
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
		[HttpPost("Edit")]
		public ActionResult Edit(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{

				String sProjectData = collection["ItemData"];
				String sProjectName = collection["project"];

				if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectName).Any()).Any())
				{
					Project project = context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.Files).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.File File = JsonConvert.DeserializeObject<Models.File>(sProjectData);
					context.Update(File);

		
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
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
		[HttpPost("Delete")]
		public ActionResult Delete(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{

				String sProjectData = collection["ItemData"];
				String sProjectName = collection["project"];

				if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectName).Any()).Any())
				{
					Project project = context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.Files).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.File files = context.files.Where(a => a.Id == long.Parse(sProjectData)).First();

					context.files.Remove(files);
					project.Files.Remove(files);
					context.projects.Update(project);
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
