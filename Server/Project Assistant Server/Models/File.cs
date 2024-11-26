using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Project_Assistant_Server.Models
{
	public class File
	{
		public String name { get; set; }
		public String description { get; set; }
		public String fileName { get; set; }
		public bool startOnce { get; set; }
		[Key]
		public long id;

	}
}
