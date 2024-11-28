using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	public class DBBase
	{
		[Key]
		public long Id { get; set; }
		public String caption { get; set; }
	}
}
