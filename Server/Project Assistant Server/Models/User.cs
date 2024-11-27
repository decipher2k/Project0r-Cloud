using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	[PrimaryKey("Id")]
	public class User
	{
		[Key]
		public long  Id { get; set; }
		public string Name { get; set; }
		public string Fullname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }		
		public string Salt { get; set; }
		public string CurrentSession { get; set; }
		public bool IsAdmin { get; set; }


		public List<Project> Projects { get; set; }
	}
}
