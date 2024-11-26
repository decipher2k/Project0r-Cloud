using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	public class Note
	{
		public String name { get; set; }
		public String description { get; set; }
		public String text { get; set; }
		[Key]
		public long id;
	}
}
