using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.Dto
{
	public class Project
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsInvited { get; set; } = false;
		public bool IsOwner { get; set; } = false;
		public List<Calendar> Calendars { get; set; }
		public List<File> Files { get; set; }
		public List<Log> Logs { get; set; }
		public List<Note> Notes { get; set; }
		public List<Program> Programs { get; set; }
		public List<ToDo> ToDo { get; set; }
	}
}
