using Newtonsoft.Json;
using Project_Assistant.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.API
{
	public class ChatAPI : APIBase
	{
		public bool SendMessage(String message, String project)
		{
			return PostCreate(message, "/api/Chat/PostChatMessage", project)!=null;
		}

		public ChatDto GetMessages(String project)
		{
			String sChat=PostFetchProjectContextual("/api/Chat/GetChatMessages",project);
			ChatDto chatDto=JsonConvert.DeserializeObject<ChatDto>(sChat);
			return chatDto;
		}
	}
}
