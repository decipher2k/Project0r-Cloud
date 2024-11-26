using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	public class ToDo
	{
		public String caption;
		public String description;
		public int priority = 99;
		public int weight = 0;
		[Key]
		public long id;
	}
}
