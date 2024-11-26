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
		public bool Create(Note item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Note/Create");

		}

		public Note Read(int Id)
		{
			String sItem = PostRead(Id, "/api/Note/Read");
			return JsonConvert.DeserializeObject<Note>(sItem);
		}

		public bool Update(Note item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Note/Update");
		}

		public bool Delete(int Id)
		{
			return PostDelete(Id, "/api/Note/Delete");
		}
	}
}
