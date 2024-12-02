using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.Dto
{
	[Serializable]
	public class ChatDto:SessionData
	{
		public List<ChatMessageDto> chatMessages { get; set; } = new List<ChatMessageDto>();
	}
}
