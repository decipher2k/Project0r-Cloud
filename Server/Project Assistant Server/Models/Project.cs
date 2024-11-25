using System.ComponentModel.DataAnnotations;

namespace Project_Assistant_Server.Models
{
	public class Project
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		public List<Calendar> Calendars { get; set; }
		public List<File> Files { get; set; }
		public List<Log> Logs { get; set; }
		public List<Note> Notes { get; set; }
		public List<Program> Programs { get; set; }
		public List<ToDo> ToDo { get; set; }
	}
}
