using Microsoft.AspNetCore.Mvc;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/Chat")]
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
			String sProject = collection["project"];;
			String session = collection["session"];

			if (context.users.Where(a => a.CurrentSession == session && a.IsAdmin == false).Any())
			{

				long lProject = context.projects.Where(a => a.Name.Equals(sProject)).First().Id;

				ChatDto chatDto = new ChatDto();

				var messages = context.chatMessages.Where(a => a.idProject == lProject);
				for (int i = messages.Count(); i > (messages.Count() > 50 ? messages.Count() - 50 : 0); i--)
				{
					chatDto.messages.Add(new ChatMessageDto()
					{
						message = messages.ElementAt(i).Message,
						timestamp = messages.ElementAt(i).timestamp,
						userName = messages.ElementAt(i).User.Fullname
					});
				}

				return Ok(chatDto);
			}
			else
			{
				return Unauthorized();
			}
		}

		[HttpPost("PostChatMessage")]
		public IActionResult PostChatMessage(IFormCollection collection)
		{
			String sProject = collection["project"];		
			String sMessage = collection["message"];
			String session = collection["session"];

			if (context.users.Where(a => a.CurrentSession == session && a.IsAdmin == false).Any())
			{

				User user = context.users.Where(a => a.CurrentSession == session).First();

				ChatMessage chatMessage = new ChatMessage();
				chatMessage.User = user;
				chatMessage.timestamp = DateTime.Now;
				chatMessage.Message = sMessage;
				chatMessage.idProject=context.projects.Where(a=>a.Name == sProject).First().Id;

				context.Add(chatMessage);
				context.SaveChanges();

				SessionData sessionData = new SessionData();
				sessionData.session=new Session(context).newSession(session);

				return Ok(sessionData);
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}
