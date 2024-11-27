using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	public class Calendar
	{
		[Key]
		public long Id { get; set; }
		public DateTime date{ get; set; }
		public DateTime from{ get; set; }
		public DateTime to{ get; set; }
		public String text{ get; set; }
		public String caption{ get; set; }
		public bool handled{ get; set; }


	}
}
