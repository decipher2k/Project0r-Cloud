using Newtonsoft.Json;
using Project_Assistant.Dto;
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
		public long Create(Note item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			IdSessionDto idSessionDto = PostCreate( sItem, "/api/Note/Create", project); return idSessionDto.Id;

		}

		public Note Read(int Id, String project)
		{
			String sItem = PostRead(Id, "/api/Note/Read", project);
			Globals.session = JsonConvert.DeserializeObject<ItemDto>(sItem).session;
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
