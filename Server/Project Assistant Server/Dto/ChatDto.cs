using Project_Assistant_Server.Models;
using System.Collections.Generic;

namespace Project_Assistant_Server.Dto
{
	public class ChatDto:SessionData
	{
		public List<ChatMessageDto> messages { get; set; } = new List<ChatMessageDto>();
	}
}
