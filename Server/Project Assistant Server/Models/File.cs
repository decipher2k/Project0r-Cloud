using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Project_Assistant_Server.Models
{
	[PrimaryKey("Id")]
	public class File
	{
		public String name { get; set; }
		public String description { get; set; }
		public String fileName { get; set; }
		public bool startOnce { get; set; }
		[Key]
		public long Id { get; set; }

	}
}
