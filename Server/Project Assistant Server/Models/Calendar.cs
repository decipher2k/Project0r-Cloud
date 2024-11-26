using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	public class Calendar
	{
		public DateTime date;
		public DateTime from;
		public DateTime to;
		public String text;
		public String caption;
		public bool handled;
		[Key]
		public long id;

	}
}
