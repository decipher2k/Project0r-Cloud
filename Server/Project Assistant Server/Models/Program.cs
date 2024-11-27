using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Project_Assistant_Server.Models
{
	[PrimaryKey("Id")]
	public class Program
	{

		[Key]
		public long Id { get; set; }
		public String name { get; set; }
		public String description { get; set; }
		public String executaleFile { get; set; }
		public bool startOnce { get; set; }


	}
}
