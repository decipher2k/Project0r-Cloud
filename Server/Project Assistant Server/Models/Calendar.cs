using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	public class Calendar : DBBase
	{

		public DateTime date{ get; set; }
		public DateTime from{ get; set; }
		public DateTime to{ get; set; }
		public String text { get; set; }
		public bool handled{ get; set; }


	}
}
