using Project_Assistant_Server.Models;

namespace Project_Assistant_Server.Dto
{
	public class UserDto:SessionData
	{
		public List <Project> projects { get; set; }
	}
}
