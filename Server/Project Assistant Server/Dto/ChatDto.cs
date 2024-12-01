using Project_Assistant_Server.Models;
using System.Collections.Generic;

namespace Project_Assistant_Server.Dto
{
	public class ChatDto
	{
		public List<ChatMessageDto> messages = new List<ChatMessageDto>();
	}
}
