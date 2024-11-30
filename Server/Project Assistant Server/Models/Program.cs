using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Project_Assistant_Server.Models
{
	[PrimaryKey("Id")]
	public class Program : DBBase
	{
		
		public String name { get => caption; set => caption = value; }
		public String description { get; set; }
		public String executaleFile { get; set; }
		public bool startOnce { get; set; }


	}
}
