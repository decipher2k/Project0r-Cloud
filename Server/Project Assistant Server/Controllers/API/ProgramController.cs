﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;
using System.Globalization;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/api/Program")]
	public class ProgramController : ControllerBase
	{
		
		DatabaseContext context;
		public ProgramController(DatabaseContext _context)
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

				Models.Program Program = context.users.Where(a => a.CurrentSession == session && a.IsAdmin==false)
					.Include(a => a.Projects).ThenInclude(a => a.Programs).First()
					.Projects.Where(a => a.Name == project).First()
					.Programs.Where(a => a.Id == iItemId).First();

				String calendarStr = JsonConvert.SerializeObject(Program);
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
						Include(a => a.Projects).ThenInclude(a => a.Programs).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.Program Program = JsonConvert.DeserializeObject<Models.Program>(sProjectData);
					project.Programs.Add(Program);
					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = Program.Id;
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
						Include(a => a.Projects).ThenInclude(a => a.Programs).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.Program Program = JsonConvert.DeserializeObject<Models.Program>(sProjectData);

					context.Update(Program);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = Program.Id;
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

				String sProjectId = collection["ItemData"];
				String sProjectName = collection["project"];

				if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectName).Any()).Any())
				{
					Project project = context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.Programs).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.Program Program = context.program.Where(a=>a.Id==long.Parse(sProjectId)).First();

					context.program.Remove(Program);
					project.Programs.Remove(Program);
					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = Program.Id;
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
