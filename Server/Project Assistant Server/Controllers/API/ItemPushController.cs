using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Assistant_Server.Dto;
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
			int iItemId = int.Parse(collection["itemId"]);
			int iReceiverId = int.Parse(collection["contextId"]);
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

		[HttpPost("AcceptItem")]
		public IActionResult AcceptItem(IFormCollection collection)
		{
			int iItemPushId = int.Parse(collection["itemId"]);
			//int iSenderId = int.Parse(collection["contextId"]);

			String project = collection["project"];
			String Session = collection["session"];

			if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{
				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == project).Any()).Any())
				{
					User newUser = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).First();
					ItemPush push = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
					switch (push.Type)
					{

						case (ItemPush.ItemType.Note):
							{
								User oldUser=context.users.Where(a=>a.Id==push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									Note note = context.notes.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									oldUser.Projects.Where(a => a.Name == project).First().Notes.Remove(note);
									newUser.Projects.Where(a => a.Name == project).First().Notes.Add(note);
									itemPush.IsAccepted = ItemPush.AcceptedDenied.Accepted;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.File):
							{
								User oldUser = context.users.Where(a => a.Id == push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									File note = context.files.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									oldUser.Projects.Where(a => a.Name == project).First().Files.Remove(note);
									newUser.Projects.Where(a => a.Name == project).First().Files.Add(note);
									itemPush.IsAccepted = ItemPush.AcceptedDenied.Accepted;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.Calendar):
							{
								User oldUser = context.users.Where(a => a.Id == push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									Calendar note = context.calendars.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									oldUser.Projects.Where(a => a.Name == project).First().Calendars.Remove(note);
									newUser.Projects.Where(a => a.Name == project).First().Calendars.Add(note);
									itemPush.IsAccepted = ItemPush.AcceptedDenied.Accepted;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.Program):
							{
								User oldUser = context.users.Where(a => a.Id == push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									Models.Program note = context.program.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									oldUser.Projects.Where(a => a.Name == project).First().Programs.Remove(note);
									newUser.Projects.Where(a => a.Name == project).First().Programs.Add(note);
									itemPush.IsAccepted = ItemPush.AcceptedDenied.Accepted;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.ToDo):
							{
								User oldUser = context.users.Where(a => a.Id == push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									ToDo note = context.toDo.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									oldUser.Projects.Where(a => a.Name == project).First().ToDo.Remove(note);
									newUser.Projects.Where(a => a.Name == project).First().ToDo.Add(note);
									itemPush.IsAccepted=ItemPush.AcceptedDenied.Accepted;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
					}
					context.Remove(context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault());
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

		[HttpPost("DenyItem")]
		public IActionResult DenyItem(IFormCollection collection)
		{
			int iItemPushId = int.Parse(collection["itemId"]);
			//int iSenderId = int.Parse(collection["contextId"]);

			String project = collection["project"];
			String Session = collection["session"];

			if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{
				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == project).Any()).Any())
				{
					User newUser = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).First();
					ItemPush push = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
					switch (push.Type)
					{

						case (ItemPush.ItemType.Note):
							{
								User oldUser = context.users.Where(a => a.Id == push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									Note note = context.notes.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									itemPush.IsAccepted = ItemPush.AcceptedDenied.Denied;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.File):
							{
								User oldUser = context.users.Where(a => a.Id == push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									File note = context.files.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									itemPush.IsAccepted = ItemPush.AcceptedDenied.Denied;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.Calendar):
							{
								User oldUser = context.users.Where(a => a.Id == push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									Calendar note = context.calendars.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									itemPush.IsAccepted = ItemPush.AcceptedDenied.Denied;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.Program):
							{
								User oldUser = context.users.Where(a => a.Id == push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									Models.Program note = context.program.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									itemPush.IsAccepted = ItemPush.AcceptedDenied.Denied;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
						case (ItemPush.ItemType.ToDo):
							{
								User oldUser = context.users.Where(a => a.Id == push.SenderId).FirstOrDefault();
								if (newUser.Projects.Where(a => a.Name == project).Any())
								{
									ItemPush itemPush = context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault();
									ToDo note = context.toDo.Where(a => a.Id == itemPush.ItemId).FirstOrDefault();
									itemPush.IsAccepted = ItemPush.AcceptedDenied.Denied;
									context.Update(itemPush);
									context.Update(newUser);
									context.Update(oldUser);
								}
								else
								{
									return BadRequest();
								}
								break;
							}
					}
					context.Remove(context.itemPush.Where(a => a.Id == iItemPushId).FirstOrDefault());
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

		[HttpPost("PollItems")]
		public IActionResult PollItems(IFormCollection collection)
		{
			String project = collection["project"];
			String Session = collection["session"];

			if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{
				if (context.users.Where(a => a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).Where(a => a.Projects.Where(a => a.Name == project).Any()).Any())
				{
					User user = context.users.Where(a => a.CurrentSession == collection["session"].ToString()).First();
					ItemPushDto itemPushDto = new ItemPushDto();
					foreach(ItemPush itemPush in context.itemPush.Where(a=>a.ReceiverId==user.Id && a.IsAccepted==ItemPush.AcceptedDenied.None))
					{
						itemPushDto.Items.Add(itemPush);
					}
					String newSession = new Session(context).newSession(collection["session"].ToString());
					itemPushDto.session=newSession;
					return Ok(itemPushDto);
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
