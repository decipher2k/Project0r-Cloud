using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	[PrimaryKey("Id")]
	public class Log : DBBase
	{
		public String description{ get; set; }
		public DateTime date{ get; set; }

	}
}
