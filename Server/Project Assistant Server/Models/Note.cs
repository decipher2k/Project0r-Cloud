using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	[PrimaryKey("Id")]
	public class Note : DBBase
	{
		public String name { get=>caption; set=>caption=value; }
		public String description { get; set; }
		public String text { get; set; }
	}
}
