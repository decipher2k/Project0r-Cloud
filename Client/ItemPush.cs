using System;

namespace Project_Assistant_Server.Models
{
	public class ItemPush
	{
		public long Id { get; set; }
		public long ItemId { get; set; }
		public long ReceiverId { get; set; }
		public ItemType Type { get; set; }
		public string Project { get; set; }

		public String Title { get; set; }
		public String SenderName { get; set; }

		public AcceptedDenied IsAccepted { get; set; } = AcceptedDenied.None;

		public enum ItemType
		{
			Calendar,
			File,
			Log,
			Note,
			Program,
			ToDo,
			User
		}

		public enum AcceptedDenied
		{
			None,
			Denied,
			Accepted		
		}
		public bool HasReminded { get; set; } =false;
		
	}
}
