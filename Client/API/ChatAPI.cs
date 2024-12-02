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
			IdSessionDto idSessionDto= PostCreate(message, "/api/Chat/PostChatMessage", project);
			if (idSessionDto != null)
			{
			//	Globals.session = idSessionDto.session;
				return true;
			}
			else
			{
				return false;
			}

		}

		public List<ChatMessageDto> GetMessages(String project)
		{
			if (project != null && project!="")
			{
				String sChat = PostFetchProjectContextual("/api/Chat/GetChatMessages", project);
				if (sChat != "ERROR")
				{
					List<ChatMessageDto> chatDto= JsonConvert.DeserializeObject<List<ChatMessageDto>>(sChat);
					return chatDto;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;	
			}
		}
	}
}
