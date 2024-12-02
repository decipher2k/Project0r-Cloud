using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/api/Chat")]
	public class ChatController : ControllerBase
	{
		DatabaseContext context;
		public ChatController(DatabaseContext _context) 
		{ 
			context = _context;
		}


		[HttpPost("GetChatMessages")]
		public IActionResult GetChatMessages(IFormCollection collection)
		{
			if (collection != null)
			{
				String sProject = collection["project"];
				String session = collection["session"];

				if (context.users.Where(a => a.CurrentSession == session && a.IsAdmin == false).Any())
				{
					ChatDto chatDto = new ChatDto();
					chatDto.messages = new List<ChatMessageDto>();

					foreach (Project project in context.projects.Where(a => a.Name == sProject).ToList())
					{
						var messages = context.chatMessages.Where(a => a.idProject == project.Id).Include(a => a.User).ToList();
						int start = 0;
						if (messages.Count() > 50)
							start = messages.Count() - 50;
						if (messages.Count() > 0)
						{
							foreach (ChatMessage msg in messages.ToList().Take(new Range(start, messages.Count() - 1)))
							{
								chatDto.messages.Add(new ChatMessageDto()
								{
									message = msg.Message,
									timestamp = msg.timestamp,
									userName = msg.User.Fullname
								});
							}
						}
					}
					chatDto.messages=chatDto.messages.OrderBy(a=>a.timestamp).ToList();
					//long lProject = context.users.Where(a => a.CurrentSession == session && a.IsAdmin == false).Include(a=>a.Projects).First().Projects.Where(a => a.Name==sProject).First().Id;

					

					

				
					//chatDto.session = new Session(context).newSession(session);
					return Ok(chatDto.messages);
				}
				else
				{
					return Unauthorized();
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost("PostChatMessage")]
		public IActionResult PostChatMessage(IFormCollection collection)
		{
			String sProject = collection["project"];		
			String sMessage = collection["ItemData"];
			String session = collection["session"];

			if (context.users.Where(a => a.CurrentSession == session && a.IsAdmin == false).Any())
			{

				User user = context.users.Where(a => a.CurrentSession == session).Include(a=>a.Projects).First();

				ChatMessage chatMessage = new ChatMessage();
				chatMessage.User = user;
				chatMessage.timestamp = DateTime.Now;
				chatMessage.Message = sMessage;
				
				chatMessage.idProject=user.Projects.Where(a=>a.Name == sProject).First().Id;

				context.Add(chatMessage);
				context.SaveChanges();

				//SessionData sessionData = new SessionData();
				//sessionData.session=new Session(context).newSession(session);

				return Ok();
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}
