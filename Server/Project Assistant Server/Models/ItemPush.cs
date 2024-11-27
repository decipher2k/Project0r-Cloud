namespace Project_Assistant_Server.Models
{
	public class ItemPush
	{
		public long Id { get; set; }
		public long ItemId { get; set; }
		public long ReceiverId { get; set; }
		public ItemType Type { get; set; }

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
	}
}
