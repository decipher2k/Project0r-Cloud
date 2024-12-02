using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.Dto
{
	[Serializable]
	public class ChatMessageDto
	{
		public string message { get; set; }
		public String userName { get; set; }
		public DateTime timestamp { get; set; }
	}
}
