﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Assistant_Server.Models;
using File = Project_Assistant_Server.Models.File;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/api/ItemPush")]
	public class ItemPushController : ControllerBase
	{
		private DatabaseContext context;

		public ItemPushController(DatabaseContext context)
		{
			this.context = context;
		}

		[HttpPost("PushItem")]
		public IActionResult PushItem(IFormCollection collection)
		{
			int iItemId = int.Parse(collection["ItemId"]);
			int iReceiverId = int.Parse(collection["ReceiverId"]);
			String project = collection["project"];
			String Session = collection["session"];

			if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{
				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == project).Any()).Any())
				{
					ItemPush push = new ItemPush();
					push.ItemId = iItemId;
					push.ReceiverId = iReceiverId;
					context.Add(push);
					context.SaveChanges();
					return Ok();
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

		[HttpPost("AcceptItem")]
		public IActionResult AcceptItem(IFormCollection collection)
		{
			int iItemPushId = int.Parse(collection["ItemPushId"]);
			int iSenderId = int.Parse(collection["ItemSenderId"]);

			String project = collection["project"];
			String Session = collection["session"];

			if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{
				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == project).Any()).Any())
				{
					User user = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).First();
					ItemPush push=context.itemPush.Where(a=>a.Id== iItemPushId).FirstOrDefault();
					switch (push.Type)
					{

						case (ItemPush.ItemType.Note):
							{
								User newUser = context.users.Where(a => a.Id == iSenderId).First();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									Note note = context.notes.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									user.Projects.Where(a => a.Name == project).First().Notes.Remove(note);

									newUser.Projects.Where(a => a.Name == project).First().Notes.Add(note);
									context.Update(user);
									context.Update(newUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.File):
							{
								User newUser = context.users.Where(a => a.Id == iSenderId).First();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									File note = context.files.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									user.Projects.Where(a => a.Name == project).First().Files.Remove(note);

									newUser.Projects.Where(a => a.Name == project).First().Files.Add(note);
									context.Update(user);
									context.Update(newUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.Calendar):
							{
								User newUser = context.users.Where(a => a.Id == iSenderId).First();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									Calendar note = context.calendars.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									user.Projects.Where(a => a.Name == project).First().Calendars.Remove(note);
									newUser.Projects.Where(a => a.Name == project).First().Calendars.Add(note);
									context.Update(user);
									context.Update(newUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.Program):
							{ 
								User newUser = context.users.Where(a => a.Id == iSenderId).First();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									Models.Program note = context.program.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									user.Projects.Where(a => a.Name == project).First().Programs.Remove(note);
									newUser.Projects.Where(a => a.Name == project).First().Programs.Add(note);
									context.Update(user);
									context.Update(newUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.ToDo):
							{
								User newUser = context.users.Where(a => a.Id == iSenderId).First();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									ToDo note = context.toDo.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									user.Projects.Where(a => a.Name == project).First().ToDo.Remove(note);
									newUser.Projects.Where(a => a.Name == project).First().ToDo.Add(note);
									context.Update(user);
									context.Update(newUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
							break;
					}
					return Ok();
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