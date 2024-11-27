﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/api/Note")]
	public class NoteController : ControllerBase
	{

		DatabaseContext context;
		public NoteController(DatabaseContext _context)
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

				Models.Note Note = context.users.Where(a => a.CurrentSession == session)
					.Include(a => a.Projects).ThenInclude(a => a.Notes).First()
					.Projects.Where(a => a.Name == project).First()
					.Notes.Where(a => a.Id == iItemId).First();

				return Ok(Note);
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

				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectName).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.Notes).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.Note Note = JsonConvert.DeserializeObject<Note>(sProjectData);
					project.Notes.Add(Note);
					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = Note.Id;
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

				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectName).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.Notes).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.Note Note = JsonConvert.DeserializeObject<Models.Note>(sProjectData);
					for (int i = 0; i < project.Notes.Count; i++)
					{
						if (project.Notes[i].Id == Note.Id)
						{
							project.Notes[i] = Note;
							break;
						}
					}

					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = Note.Id;
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

				String sProjectData = collection["ItemData"];
				String sProjectName = collection["project"];

				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == sProjectName).Any()).Any())
				{
					Project project = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).
						Include(a => a.Projects).ThenInclude(a => a.Notes).First()
						.Projects.Where(a => a.Name == sProjectName).First();

					Models.Note notes = context.notes.Where(a => a.Id == long.Parse(sProjectData)).First();

					context.notes.Remove(notes);
					project.Notes.Remove(notes);
					context.projects.Update(project);
					context.SaveChanges();

					context.projects.Update(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
					IdSessionDto sessionData = new IdSessionDto();
					sessionData.session = newSession;
					sessionData.Id = Note.Id;
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
