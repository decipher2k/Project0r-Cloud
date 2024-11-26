using Newtonsoft.Json;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.API
{
	public class NoteAPI : APIBase
	{
		public bool Create(Note item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Note/Create", project);

		}

		public Note Read(int Id, String project)
		{
			String sItem = PostRead(Id, "/api/Note/Read", project);
			return JsonConvert.DeserializeObject<Note>(sItem);
		}

		public bool Update(Note item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/Note/Update", project);
		}

		public bool Delete(int Id, String project)
		{
			return PostDelete(Id, "/api/Note/Delete", project);
		}
	}
}
