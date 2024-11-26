using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Project_Assistant_Server.Models
{
	public class Program
	{
		public String name { get; set; }
		public String description { get; set; }
		public String executaleFile { get; set; }
		public bool startOnce { get; set; }

		[Key]
		public long id;

	}
}
