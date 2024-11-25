using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }		
		public string CurrentSession { get; set; }

		public List<Project> Projects { get; set; }
	}
}
