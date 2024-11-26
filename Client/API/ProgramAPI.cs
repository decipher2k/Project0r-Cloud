using Newtonsoft.Json;
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
		public bool Create(Program item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Program/Create", project);

		}

		public Program Read(int Id, String project)
		{
			String sItem = PostRead(Id, "/api/Program/Read", project);
			return JsonConvert.DeserializeObject<Program>(sItem);
		}

		public bool Update(Program item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/Program/Update", project);
		}

		public bool Delete(int Id, String project)
		{
			return PostDelete(Id, "/api/Program/Delete", project);
		}
	}
}
