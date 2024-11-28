using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	[PrimaryKey("Id")]
	public class ToDo : DBBase
	{
		public String description{ get; set; }
		public int priority { get; set; } = 99;
		public int weight { get; set; } = 0;
	}
}
