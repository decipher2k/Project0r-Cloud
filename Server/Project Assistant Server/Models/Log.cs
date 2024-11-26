using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	public class Log
	{
		public String caption;
		public String description;
		public DateTime date;
		[Key]
		public long id;

	}
}
