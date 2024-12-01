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
	public class ProgramAPI : APIBase
	{
		public long Create(Program item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			IdSessionDto idSessionDto = PostCreate( sItem, "/api/Program/Create", project); return idSessionDto.Id;

		}

		public Program Read(long Id, String project)
		{
			String sItem = PostRead(Id, "/api/Program/Read", project);
			Globals.session = JsonConvert.DeserializeObject<ItemDto>(sItem).session;
			return JsonConvert.DeserializeObject<Program>(sItem);
		}

		public bool Update(Program item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/Program/Update", project);
		}

		public bool Delete(long Id, String project)
		{
			return PostDelete(Id, "/api/Program/Delete", project);
		}
	}
}
