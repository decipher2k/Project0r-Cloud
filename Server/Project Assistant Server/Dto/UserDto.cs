using Project_Assistant_Server.Models;

namespace Project_Assistant_Server.Dto
{
	public class UserDto
	{
		public String session { get; set; }
		public List <Project> projects { get; set; }
	}
}
