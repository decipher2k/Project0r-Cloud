using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	[PrimaryKey("Id")]
	public class Note
	{
		public String name { get; set; }
		public String description { get; set; }
		public String text { get; set; }
		[Key]
		public long Id { get; set; }
	}
}
