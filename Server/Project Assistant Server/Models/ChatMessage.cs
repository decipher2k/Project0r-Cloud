namespace Project_Assistant_Server.Models
{
	public class ChatMessage
	{
		public long Id { get; set; }
		public long idProject { get; set; }
		public User User { get; set; }
		public string Message { get; set; }			
		public DateTime timestamp { get; set; }
	}
}
